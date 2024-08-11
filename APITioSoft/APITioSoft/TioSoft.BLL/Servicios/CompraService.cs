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

    }
}
