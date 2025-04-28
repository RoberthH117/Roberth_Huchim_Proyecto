using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.Services.ConfiguracionCFDI;
using QualfixAdmin.Services.RegimenFiscalService;


namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegimenFiscalController : ControllerBase
    {
        private readonly RegimeFiscalService _RegimenFiscalservice;

        public RegimenFiscalController(RegimeFiscalService regimenFiscal)
        {
            _RegimenFiscalservice = regimenFiscal;
        }

        [HttpGet]
        public IActionResult ObtenerRegimenFiscal()
        {
            try
            {
                var  regimenFiscal= _RegimenFiscalservice.ObtenerTodasInformacionFiscals();
                return Ok(regimenFiscal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }
    }
}
