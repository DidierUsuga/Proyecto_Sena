using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consultar();

                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation)
                                                  .OrderByDescending(p => p.FechaRegistro)
                                                  .ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());

            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                // Verificar si ya existe un producto con el mismo nombre
                var productoExistente = await _productoRepositorio.Obtener(p => p.Nombre == modelo.Nombre);

                if (productoExistente != null)
                {
                    throw new Exception("Ya existe un producto con el mismo nombre.");
                }

                // Establecer el stock en 0 antes de crear el nuevo producto
                modelo.Stock = 0;

                // Si no existe, proceder a crear el nuevo producto
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }

                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoEncontrado = await _productoRepositorio.Obtener(u =>
                    u.IdProducto == modelo.IdProducto
                );

                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe");
                }

                // Verificar si existe otro producto con el mismo nombre pero diferente ID
                var productoExistente = await _productoRepositorio.Obtener(u =>
                    u.Nombre == modelo.Nombre && u.IdProducto != modelo.IdProducto
                );

                if (productoExistente != null)
                {
                    throw new TaskCanceledException("Ya existe otro producto con ese nombre");
                }

                // Guardar el valor actual de Stock
                var stockActual = productoEncontrado.Stock;

                var productoModelo = _mapper.Map<Producto>(modelo);

                // Restaurar el valor de Stock con el valor actual
                productoModelo.Stock = stockActual;

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar");
                }

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

                var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == id);

                if(productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                bool respuesta = await _productoRepositorio.Eliminar(productoEncontrado);


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
