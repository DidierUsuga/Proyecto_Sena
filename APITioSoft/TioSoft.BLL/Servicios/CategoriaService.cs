using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AutoMapper;
using TioSoft.BLL.Servicios.Contrato;
using TioSoft.DAL.Repositorios.Contrato;
using TioSoft.DTO;
using TioSoft.Model;


namespace TioSoft.BLL.Servicios
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _categoriaRepositorio;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            try {

                var listaCategorias = await _categoriaRepositorio.Consultar();
                return _mapper.Map<List<CategoriaDTO>>(listaCategorias.ToList());
            
            } catch {
                throw;
            }
        }

        public async Task<CategoriaDTO> Crear(CategoriaDTO modelo)
        {
            try
            {
                // Verificar si ya existe una categoria con el mismo nombre
                var productoExistente = await _categoriaRepositorio.Obtener(p => p.Nombre == modelo.Nombre);

                if (productoExistente != null)
                {
                    throw new Exception("Ya existe una categoria con el mismo nombre.");
                }

                var categoriaCreado = await _categoriaRepositorio.Crear(_mapper.Map<Categoria>(modelo));

                if (categoriaCreado.IdCategoria == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<CategoriaDTO>(categoriaCreado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(CategoriaDTO modelo)
        {
            try
            {

                var categoriaModelo = _mapper.Map<Categoria>(modelo);
                var categoriaEncontrado = await _categoriaRepositorio.Obtener(u =>
                    u.IdCategoria == categoriaModelo.IdCategoria
                );

                if (categoriaEncontrado == null)
                    throw new TaskCanceledException("La categoria no existe");


                categoriaEncontrado.Nombre = categoriaModelo.Nombre;
                categoriaEncontrado.EsActivo = categoriaModelo.EsActivo;

                bool respuesta = await _categoriaRepositorio.Editar(categoriaEncontrado);

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

                var categoriaEncontrado = await _categoriaRepositorio.Obtener(p => p.IdCategoria == id);

                if (categoriaEncontrado == null)
                    throw new TaskCanceledException("La categoria no existe");

                bool respuesta = await _categoriaRepositorio.Eliminar(categoriaEncontrado);


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
