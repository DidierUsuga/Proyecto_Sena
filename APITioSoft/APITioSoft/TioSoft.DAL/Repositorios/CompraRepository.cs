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
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {

        private readonly TioSoftAngularContext _dbcontext;

        public CompraRepository(TioSoftAngularContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Compra> Registrar(Compra modelo)
        {
            Compra compraGenerada = new Compra();

            using (var trasaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {

                    foreach (DetalleCompra dv in modelo.DetalleCompra)
                    {

                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock + dv.Cantidad;
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
                    string numeroCompra = ceros + correlativo.UltimoNumero.ToString();
                    //00001
                    numeroCompra = numeroCompra.Substring(numeroCompra.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroCompra;

                    await _dbcontext.Compra.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    compraGenerada = modelo;

                    trasaction.Commit();

                }
                catch
                {

                    trasaction.Rollback();
                    throw;
                }

                return compraGenerada;

            }



        }
    }
}
