using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TioSoft.BLL.Servicios.Contrato;
using TioSoft.DAL.Repositorios.Contrato;
using TioSoft.DTO;
using TioSoft.Model;

namespace TioSoft.BLL.Servicios
{
    public class ProveedorService : IProveedorService
    {
        private readonly IGenericRepository<Proveedor> _proveedorRepositorio;
        private readonly IMapper _mapper;

        public ProveedorService(IGenericRepository<Proveedor> proveedorRepositorio, IMapper mapper)
        {
            _proveedorRepositorio = proveedorRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProveedorDTO>> Lista()
        {
            try
            {

                var queryProveedor = await _proveedorRepositorio.Consultar();

                var listaProveedores = queryProveedor.Include(cat => cat.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProveedorDTO>>(listaProveedores.ToList());

            }
            catch
            {
                throw;
            }
        }

        public async Task<ProveedorDTO> Crear(ProveedorDTO modelo)
        {
            try
            {
                // Verificar si ya existe un proveedor con el mismo correo
                var proveedorExistente = await _proveedorRepositorio.Obtener(p => p.Correo == modelo.Correo);

                if (proveedorExistente != null)
                {
                    throw new Exception("Ya existe un proveedor con el mismo correo.");
                }

                // Verificar si ya existe un proveedor con el mismo nombre
                var proveedorExistente2 = await _proveedorRepositorio.Obtener(p => p.Nombre == modelo.Nombre);

                if (proveedorExistente2 != null)
                {
                    throw new Exception("Ya existe un proveedor con el mismo nombre.");
                }

                var proveedorCreado = await _proveedorRepositorio.Crear(_mapper.Map<Proveedor>(modelo));

                if (proveedorCreado.IdProveedor == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<ProveedorDTO>(proveedorCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProveedorDTO modelo)
        {
            try
            {

                var proveedorModelo = _mapper.Map<Proveedor>(modelo);
                var proveedorEncontrado = await _proveedorRepositorio.Obtener(u =>
                    u.IdProveedor == proveedorModelo.IdProveedor
                );

                if (proveedorEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");


                proveedorEncontrado.Nombre = proveedorModelo.Nombre;
                proveedorEncontrado.IdCategoria = proveedorModelo.IdCategoria;
                proveedorEncontrado.Direccion = proveedorModelo.Direccion;
                proveedorEncontrado.Telefono= proveedorModelo.Telefono;
                proveedorEncontrado.Correo= proveedorModelo.Correo;
                proveedorEncontrado.EsActivo = proveedorModelo.EsActivo;

                bool respuesta = await _proveedorRepositorio.Editar(proveedorEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar"); ;


                return respuesta;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {

                var proveedorEncontrado = await _proveedorRepositorio.Obtener(p => p.IdProveedor == id);

                if (proveedorEncontrado == null)
                    throw new TaskCanceledException("El proveedor no existe");

                bool respuesta = await _proveedorRepositorio.Eliminar(proveedorEncontrado);


                if (!respuesta)
                    throw new TaskCanceledException("No se pudo elminar"); ;

                return respuesta;

            }
            catch
            {
                throw;
            }
        }

    }
}
