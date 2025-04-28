using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.Services.CiudadService;
using QualfixAdmin.Services.PaisService;

namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly CiudadService _ciudadService;

        public CiudadController(CiudadService ciudadService) 
        {
            _ciudadService = ciudadService;
        }

        [HttpGet]
        public IActionResult GetCiudades()
        {
            try
            {
                var ciudades = _ciudadService.ObtenerTodosCiudades();
                return Ok(ciudades);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

    }
}
