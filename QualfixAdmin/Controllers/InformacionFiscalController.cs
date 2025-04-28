using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.model.QualfixAdminData;
using QualfixAdmin.Services.ClienteService;
using QualfixAdmin.Services.InformacionFiscal;

namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionFiscalController : ControllerBase
    {
        private readonly InformacionFiscalService _InfoFiscalservice;

        public InformacionFiscalController(InformacionFiscalService informacionFiscalService)
        {
            _InfoFiscalservice = informacionFiscalService;
        }

        [HttpGet]
        public IActionResult obtenerInformacionesFiscales() 
        {
            try
            {
                var usuarios = _InfoFiscalservice.ObtenerTodasInformacionesFiscales();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerInfoFiscalPorId(int id)
        {
            try
            {
                var info = _InfoFiscalservice.ObtenerinfoFiscalPorId(id);
                if (info == null)
                {
                    return NotFound();
                }
                return Ok(info);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult crearInformacionFiscal([FromBody] InformacionFiscals infoFiscal)
        {
            try
            {
                // Agrega la información fiscal y obtén el ID del nuevo registro
                var nuevoId = _InfoFiscalservice.AgregarInformacionFiscal(infoFiscal);

                // Devuelve el ID como parte de la respuesta
                return Ok(new {nuevoId});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarInformacionFiscal(int id, [FromBody] InformacionFiscals infoFiscal)
        {
            try
            {
                var existingUsuario = _InfoFiscalservice.ObtenerinfoFiscalPorId(id);

                if (existingUsuario == null)
                {
                    return NotFound();
                }

                infoFiscal.InformacionFiscalId = id;
                _InfoFiscalservice.ActualizarInformacionFiscal(infoFiscal);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarInformacionFiscal(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("El ID proporcionado no es válido.");
                }

                var usuario = _InfoFiscalservice.ObtenerinfoFiscalPorId(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _InfoFiscalservice.EliminarInformacionFiscal(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

    }
}
