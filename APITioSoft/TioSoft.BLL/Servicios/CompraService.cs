using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TioSoft.BLL.Servicios.Contrato;
using TioSoft.DAL.Repositorios.Contrato;
using TioSoft.DTO;
using TioSoft.Model;


namespace TioSoft.BLL.Servicios
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepositorio;
        private readonly IGenericRepository<DetalleCompra> _detalleCompraRepositorio;
        private readonly IMapper _mapper;

        public CompraService(ICompraRepository compraRepositorio,
            IGenericRepository<DetalleCompra> detalleCompraRepositorio,
            IMapper mapper)
        {
            _compraRepositorio = compraRepositorio;
            _detalleCompraRepositorio = detalleCompraRepositorio;
            _mapper = mapper;
        }


        public async Task<CompraDTO> Registrar(CompraDTO modelo)
        {
            try
            {
                var compraGenerada = await _compraRepositorio.Registrar(_mapper.Map<Compra>(modelo));

                if (compraGenerada.IdCompra == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<CompraDTO>(compraGenerada);

            }
            catch
            {
                throw;
            }
        }


        public async Task<List<CompraDTO>> Historial(string buscarPor, string numeroCompra, string fechaInicio, string fechaFin)
        {
            IQueryable<Compra> query = await _compraRepositorio.Consultar();
            var ListaResultado = new List<Compra>();

            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                    ListaResultado = await query
                        .Where(v =>
                            v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                            v.FechaRegistro.Value.Date <= fech_Fin.Date
                        )
                        .Include(dv => dv.DetalleCompra)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .Include(v => v.IdProveedorNavigation) // Incluye la información del proveedor
                        .ToListAsync();
                }
                else
                {
                    ListaResultado = await query
                        .Where(v => v.NumeroDocumento == numeroCompra)
                        .Include(dv => dv.DetalleCompra)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .Include(v => v.IdProveedorNavigation) // Incluye la información del proveedor
                        .ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<CompraDTO>>(ListaResultado);
        }


    }
}
