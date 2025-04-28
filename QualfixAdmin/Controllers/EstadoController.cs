using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.Services.EstadoService;
using QualfixAdmin.Services.PaisService;

namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly EstadoService _stadoService;

        public EstadoController(EstadoService stadoService)
        {
            _stadoService = stadoService;
        }

        [HttpGet]
        public IActionResult GetEstados()
        {
            try
            {
                var estados = _stadoService.ObtenerTodosEstados();
                return Ok(estados);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

    }
}
