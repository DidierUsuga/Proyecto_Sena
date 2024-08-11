using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TioSoft.BLL.Servicios.Contrato;
using TioSoft.DTO;
using TioSoft.API.Utilidad;

namespace TioSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {

        private readonly ICompraService _compraServicio;

        public CompraController(ICompraService compraServicio)
        {
            _compraServicio = compraServicio;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] CompraDTO compra)
        {
            var rsp = new Response<CompraDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _compraServicio.Registrar(compra);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroCompra, string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<CompraDTO>>();
            numeroCompra = numeroCompra is null ? "" : numeroCompra;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rsp.status = true;
                rsp.value = await _compraServicio.Historial(buscarPor, numeroCompra, fechaInicio, fechaFin);

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
