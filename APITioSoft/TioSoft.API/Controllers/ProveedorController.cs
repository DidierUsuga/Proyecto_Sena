using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TioSoft.BLL.Servicios.Contrato;
using TioSoft.DTO;
using TioSoft.API.Utilidad;

namespace TioSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorServicio;

        public ProveedorController(IProveedorService proveedorServicio)
        {
            _proveedorServicio = proveedorServicio;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ProveedorDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _proveedorServicio.Lista();

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
        public async Task<IActionResult> Guardar([FromBody] ProveedorDTO proveedor)
        {
            var rsp = new Response<ProveedorDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _proveedorServicio.Crear(proveedor);

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
        public async Task<IActionResult> Editar([FromBody] ProveedorDTO proveedor)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _proveedorServicio.Editar(proveedor);

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
                rsp.value = await _proveedorServicio.Eliminar(id);

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
