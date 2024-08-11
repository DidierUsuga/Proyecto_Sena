using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TioSoft.DAL.DBContext;
using TioSoft.DAL.Repositorios.Contrato;
using TioSoft.Model;

namespace TioSoft.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {

        private readonly TioSoftAngularContext _dbcontext;

        public VentaRepository(TioSoftAngularContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.FirstOrDefault(p => p.IdProducto == dv.IdProducto);

                        if (producto_encontrado == null)
                        {
                            throw new InvalidOperationException($"El producto con ID {dv.IdProducto} no se encuentra en la base de datos.");
                        }

                        if (dv.Cantidad > producto_encontrado.Stock)
                        {
                            throw new InvalidOperationException($"No hay suficiente stock para el producto '{producto_encontrado.Nombre}'. Stock disponible: {producto_encontrado.Stock}");
                        }

                        producto_encontrado.Stock -= dv.Cantidad;
                        _dbcontext.Productos.Update(producto_encontrado);
                    }

                    await _dbcontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaction.Commit();
                }
                catch (InvalidOperationException ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("Error al registrar la venta.", ex);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            }
        }
    }
}