using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.ForgetUsuario;
using QualfixAdmin.DataModel.Perfil_Model;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using QualfixAdmin.Services.CreacionRol;
using QualfixAdmin.Services.Perfil_Ser;
using System.Security.Claims;

namespace QualfixAdmin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private readonly PerfilServicio _service;
     
        private UserManager<User> _userManager;
        private RoleManager<Rol> _roleManager;
        private readonly QualfixAdminSecurityContext _context;
        public PerfilController(PerfilServicio service, UserManager<User> userManager, RoleManager<Rol> roleManager, QualfixAdminSecurityContext context)
        {

            _service = service;

            _userManager = userManager;

            _roleManager = roleManager;

            _context = context;
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Perfiles-accesos")]
        public async Task<ActionResult<List<PerfilConAccesosDto>>> GetPerfilAcces()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.


            try
            {



                return new JsonResult(await _service.GetPerfilesConAccesos(user));


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
        [Route("PerfilId-accesos")]
        public async Task<ActionResult<List<PerfilConAccesosDto>>> GetIdPerfilWithAcces([FromBody] PerfilIdModel IdModel)
        {

            try
            {



                return new JsonResult(await _service.GetIdPerfilAcces(IdModel));


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
        [HttpPost]
        [Route("Perfil-create")]
        public async Task<ActionResult<List<PerfilConAccesosDto>>> CreatePerfilWithAcces([FromBody] PerfilConAccesosDto CreatePerfil)
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {



                return new JsonResult(await _service.CreatePerfil(CreatePerfil,user));


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
        [Route("Accesos")]
        public async Task<IActionResult> FindAccesos()
        {


            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.




            try
            {




                return new JsonResult(await _service.FindAllAccesos(user));







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

        [Route("ObtenerPerfil")]
        public async Task<IActionResult> ObtenerPerfil()
        {



            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {




                return new JsonResult(await _service.GetNombresPerfiles(user));


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


        //[HttpPost]
        //[Route("AsignarPerfil")]
        //public async Task<IActionResult> AgregarPerfil([FromBody] AsignarRol user)
        //{

        //    ForgetUsuario nu = new ForgetUsuario();

        //    try
        //    {






        //        return new OkObjectResult(await _service.AgregarPerfil(user));

        //        // return new OkObjectResult(await _service.GetUsuario());
        //        // return Json(nu);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Exception e = ex;
        //        while (e.InnerException != null)
        //            e = e.InnerException;
        //        Console.WriteLine(e.ToString());
        //        return new BadRequestObjectResult(new { Success = false, Message = ex.Message });
        //    }






        //}


        [HttpPost]
        [Route("AccesosDisponibles")]
        public async Task<IActionResult> FindAllAcces([FromBody] PerfilIdModel Perf)
        {


            var identity = HttpContext.User.Identity as ClaimsIdentity;




            try
            {



                return new JsonResult(await _service.GetAccesosPorId(Perf));







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
        [Route("Perfil-update")]
        public async Task<ActionResult<List<PerfilConAccesosDto>>> UpdatePerfilWithAcces([FromBody] PerfilConAccesosDtoUpdate UpdatePerfil)
        {

            try
            {



                return new JsonResult(await _service.ActualizarPerfilYAccesos(UpdatePerfil));


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
        [Route("Perfil-delete")]
        public async Task<ActionResult<List<PerfilConAccesosDto>>> DeletePerfilWithAcces([FromBody] PerfilIdModel IdPerfil)
        {

            try
            {



                return new JsonResult(await _service.EliminarPerfilYAccesos(IdPerfil));


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
