using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.dal.DBContext;

using QualfixAdmin.DataModel.TicketModelo;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using QualfixAdmin.Services.Ticket;

namespace QualfixAdmin.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {


        private readonly QualfixAdminContext _context;
        private readonly TicketServices _servicio;
        private readonly UserManager<User> _userManager;


        public TicketController(QualfixAdminContext context, TicketServices servicio, UserManager<User> userManager)
        {
            _context = context;
            _servicio = servicio;
            _userManager=userManager;

        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("CreateTicket")]
        public async Task<IActionResult> CreateTicket([FromForm] TicketDto reporte)
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {

                var resultado = await _servicio.ProcesarTicket(reporte, user);

                return Ok();

                // return new OkObjectResult(await _service.GetUsuario());
                // return Json(nu);
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
        [Route("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody] ReporteModelo reporte)
        {

            // var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            //var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {

                var resultado = await _servicio.UpdateTicket(reporte);

                return Ok();

                // return new OkObjectResult(await _service.GetUsuario());
                // return Json(nu);
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




        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("FindTicket")]
        public async Task<IActionResult> FindTicket()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {






                return new OkObjectResult(await _servicio.FindTicket(user));

                // return new OkObjectResult(await _service.GetUsuario());
                // return Json(nu);
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
        [Route("GetIdTicket")]
        public async Task<IActionResult> GetIdTicket([FromBody] IdRequest id)
        {

            // var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            //var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {
               
                    var resultado = await _servicio.GetIdTicket(int.Parse(id.Id));




                return new JsonResult(resultado);
            

                // return new OkObjectResult(await _service.GetUsuario());
                // return Json(nu);
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



        [HttpGet]
        [Route("GetTicketPendiente")]
        public async Task<IActionResult> GetTicketPendiente( )
        {

            // var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            //var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {
                var Crear = _servicio.GetReporte();



                return new JsonResult(Crear);

          

                // return new OkObjectResult(await _service.GetUsuario());
                // return Json(nu);
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
