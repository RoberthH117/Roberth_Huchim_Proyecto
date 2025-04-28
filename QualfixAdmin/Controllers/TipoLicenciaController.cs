using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.DataModel.CRUDtipolicencia;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.Services.CreacionTipoLicencia;

namespace QualfixAdmin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TipoLicenciaController : ControllerBase
    {
        private readonly TipoLicencia _service;




        public TipoLicenciaController(TipoLicencia service)
        {
            _service = service; 
        }


        [HttpGet]

        [Route("GetTipoLicencia")]
        public async Task<IActionResult> GetTipoLicencia()
        {



            try
            {




                return new JsonResult(await _service.GetAllTipoLicencia());


                
            }
            catch (System.Exception ex)
            {
                Exception e = ex;
                while (e.InnerException != null)
                    e = e.InnerException;
                Console.WriteLine(e.ToString());
                return new BadRequestObjectResult(new { Success = false, Message = ex.Message });
            }






        }




        [HttpPost]

        [Route("InsertTipoLicencia")]
        public async Task<IActionResult> InsertTipoLicencia([FromBody] InsertTipoLicenciaModelo ILicencia)
        {



            try
            {




                return new JsonResult(await _service.InsertTipoLicencia(ILicencia));


            }
            catch (System.Exception ex)
            {
                Exception e = ex;
                while (e.InnerException != null)
                    e = e.InnerException;
                Console.WriteLine(e.ToString());
                return new BadRequestObjectResult(new { Success = false, Message = ex.Message });
            }






        }




        [HttpPost]

        [Route("UpdateTipoLicencia")]
        public async Task<IActionResult> UpdateTipoLicencia([FromBody] EditarTipoLicencia ULicencia)
        {



            try
            {




                return new JsonResult(await _service.UpdateTipoLicencia(ULicencia));


            }
            catch (System.Exception ex)
            {
                Exception e = ex;
                while (e.InnerException != null)
                    e = e.InnerException;
                Console.WriteLine(e.ToString());
                return new BadRequestObjectResult(new { Success = false, Message = ex.Message });
            }






        }



        [HttpPost]

        [Route("DeleteTipoLicencia")]
        public async Task<IActionResult> DeleteTipoLicencia([FromBody] EliminarTipoLicencia Eliminar)
        {



            try
            {




                return new JsonResult(await _service.DeleteTipoLicencia(Eliminar));


            }
            catch (System.Exception ex)
            {
                Exception e = ex;
                while (e.InnerException != null)
                    e = e.InnerException;
                Console.WriteLine(e.ToString());
                return new BadRequestObjectResult(new { Success = false, Message = ex.Message });
            }






        }


        [HttpPost]

        [Route("GetIdTipoLicencia")]
        public async Task<IActionResult> GetIdTipoLicencia([FromBody] EliminarTipoLicencia Eliminar)
        {



            try
            {




                return new JsonResult(await _service.GetIdTipoLicencia(Eliminar));


            }
            catch (System.Exception ex)
            {
                Exception e = ex;
                while (e.InnerException != null)
                    e = e.InnerException;
                Console.WriteLine(e.ToString());
                return new BadRequestObjectResult(new { Success = false, Message = ex.Message });
            }






        }


    }
}
