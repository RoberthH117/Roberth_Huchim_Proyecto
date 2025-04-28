using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.model.QualfixAdminData;
using QualfixAdmin.Services.ClienteService;
using QualfixAdmin.Services.ConfiguracionCFDI;
using QualfixAdmin.Services.InformacionFiscal;

namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionCDFIController : ControllerBase
    {
        private readonly ConfiguracionCFDIService _Configuracionservice;

        public ConfiguracionCDFIController(ConfiguracionCFDIService configuracionCFDI)
        {
            _Configuracionservice = configuracionCFDI;
        }

        [HttpGet]
        public IActionResult ObtenerConfigCDFI()
        {
            try
            {
                var usuarios = _Configuracionservice.ObtenerTodasConfiguracionesCFDI();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult ObtenerConfigCDFIid(int id)
        {
            try
            {
                var config =_Configuracionservice.ObtenerConfiguracionCFDI(id);
                return Ok(config);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult CrearConfigCDFI([FromBody] ConfiguracionCfdi infoFiscal)
        {
            try
            {
                _Configuracionservice.AgregarConfiguracionCFDI(infoFiscal);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public IActionResult ActualizarConfigCDFI(int id, [FromBody] ConfiguracionCfdi configuracionCfdi)
        {
            try
            {
                var existingUsuario = _Configuracionservice.ObtenerConfiguracionCFDI(id);

                if (existingUsuario == null)
                {
                    return NotFound();
                }

                configuracionCfdi.Id = id;
                _Configuracionservice.ActualizarConfiguracionCFDI(configuracionCfdi);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarConfiguracionCFDI(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("El ID proporcionado no es válido.");
                }

                var usuario = _Configuracionservice.ObtenerConfiguracionCFDI(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _Configuracionservice.EliminarConfiguracionCFDI(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

    }
}
