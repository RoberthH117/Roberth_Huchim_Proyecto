using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.Services.PaisService;
using QualfixAdmin.Services.Producto;

namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly PaisService _paisService;

        public PaisController(PaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet]
        public IActionResult GetPaises() 
        {
            try
            {
                var paises = _paisService.ObtenerTodosPaises();
                return Ok(paises);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }


    }
}
