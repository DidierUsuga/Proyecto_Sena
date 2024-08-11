using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TioSoft.BLL.Servicios.Contrato;
using TioSoft.DTO;
using TioSoft.API.Utilidad;

namespace TioSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaServicio;
        public CategoriaController(ICategoriaService categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _categoriaServicio.Lista();

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CategoriaDTO categoria)
        {
            var rsp = new Response<CategoriaDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _categoriaServicio.Crear(categoria);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);

        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CategoriaDTO categoria)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _categoriaServicio.Editar(categoria);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }


        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _categoriaServicio.Eliminar(id);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);

        }

    }
}
