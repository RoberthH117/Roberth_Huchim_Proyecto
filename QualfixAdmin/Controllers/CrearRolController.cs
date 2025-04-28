using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.DataModel.ForgetUsuario;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using QualfixAdmin.Services.CreacionRol;
using QualfixAdmin.Services.Login;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using QualfixAdmin.DataModel.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QualfixAdmin.DataModel.Perfil_Model;
using QualfixAdmin.dal.DBContext;
using System.Runtime.ConstrainedExecution;

namespace QualfixAdmin.Controllers
{
   
   
   
    [Route("[controller]")]
//    [EnableCors("AllowSpecificOrigins")]
    [ApiController]


    public class CrearRolController : ControllerBase
    {


        private readonly RolService _service;
        private readonly string secretkey;
        private UserManager<User> _userManager;
        private RoleManager<Rol> _roleManager;
        private readonly QualfixAdminSecurityContext _context;
        public CrearRolController(RolService service, IConfiguration config, UserManager<User> userManager, RoleManager<Rol> roleManager, QualfixAdminSecurityContext context)
        {

            _service = service;
            
            _userManager = userManager;

            _roleManager = roleManager;

            _context=context;
        }








        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
 
        [Route("NuevoRol")]
        public async Task<IActionResult> CrearRol([FromBody] CrearRolRequest rol)
        {

       

            try
            {

                var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
                //var idCompany = User.Claims.FirstOrDefault(c => c.Type == "Id_Company").Value;
                
                var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.
                

                return new JsonResult(await _service.CrearRol(rol,user));


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
        
        [Route("UpdateRol")]
        public async Task<IActionResult> UpdateRol([FromBody] UpdateRolRequest rol)
        {



            try
            {


                

                return new JsonResult(await _service.UpdateRol(rol));


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

        [Route("UpdateRolPermisos")]
        public async Task<IActionResult> UpdateRolPermisos([FromBody] UpdateRolRequest rol)
        {



            try
            {




                return new JsonResult(await _service.UpdateRolPermisos(rol));


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

        [Route("ObtenerRol")]
        public async Task<IActionResult> ObtenerRol()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            //var idCompany = User.Claims.FirstOrDefault(c => c.Type == "Id_Company").Value;

            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {




                return new JsonResult(await _service.GetNombresRoles(user));


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

        [Route("ObtenerNombre")]
        public async Task<IActionResult> ObtenerNombre()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            //var idCompany = User.Claims.FirstOrDefault(c => c.Type == "Id_Company").Value;

            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {




                return new JsonResult(await _service.ObtenerNombresComerciales(user));


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

        [Route("EliminarRol")]
        public async Task<IActionResult> DeleteRol([FromBody] DeleteRolRequest rol)
        {



            try
            {




                return new JsonResult(await _service.DeleteRol(rol));


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

        [Route("EliminarRolConPermisos")]
        public async Task<IActionResult> EliminarRolConPermisos([FromBody] DeleteRolRequest rol)
        {



            try
            {




                return new JsonResult(await _service.DeleteRolWithPermissions(rol));


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
        //[Route("AsignarRol")]
        //public async Task<IActionResult> AgregarRol([FromBody] AsignarRol user)
        //{

        //    ForgetUsuario nu = new ForgetUsuario();

        //    try
        //    {






        //        return new OkObjectResult(await _service.AgregarRol(user));

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

        [Route("FindRol")]
        public async Task<IActionResult> FindRolId([FromBody] DeleteRolRequest rol)
        {



            try
            {




                return new JsonResult(await _service.FindRolId(rol));


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
        [Route("FindAllRol")]
        public async Task<IActionResult> FindAllRolId()
      {


           


            

            try
            {

              

                    return new JsonResult(await _service.FindAllRolId());
                

                

             


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
        [Route("FindAllPermissions")]
        public async Task<IActionResult> FindAllPermissions()
        {


            var identity = HttpContext.User.Identity as ClaimsIdentity;




            try
            {



                    return new JsonResult(await _service.FindAllPermissions());
                






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
        
        [Route("FindAllUserPermissions")]
        public async Task<IActionResult> FindAllUserPermissions()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
                var idCompany = User.Claims.FirstOrDefault(c => c.Type == "role").Value;

                var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.
                var rol=await _roleManager.FindByNameAsync(idCompany);

                var rolesWithPermissions = await _service.GetRolesWithPermissionsAsync(user, rol);

                return new JsonResult(rolesWithPermissions);
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

        [Route("FindRolPermissions")]
        public async Task<IActionResult> FindRolPermissions([FromBody] DeleteRolRequest rol)
        {



            try
            {




                return new JsonResult(await _service.GetRoleIdWithPermissionsAsync(rol));


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

        [Route("RolDisponible")]
        public async Task<IActionResult> RolDisponible([FromBody] DeleteRolRequest rol)
        {



            try
            {




                return new JsonResult(await _service.GetPermisosDeRolAsync(rol));


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
        [Route("Comprobar")]
        public async Task<bool> Comprobar()
        {



            return true;

        }




        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Permisos")]
        public async Task<PermisosUsu> Permisos()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var idRol = User.Claims.FirstOrDefault(c => c.Type == "role").Value;
            var idPerfil = User.Claims.FirstOrDefault(c => c.Type == "PerfilId")?.Value;

            var id = int.TryParse(idPerfil, out int perfilIdInt);
            var per = _context.Perfiles.FirstOrDefault(c => c.PerfilId == perfilIdInt);
            //var idCompany = User.Claims.FirstOrDefault(c => c.Type == "Id_Company").Value;

            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.
            var rol = await _roleManager.FindByNameAsync(idRol);
       


          



            return await _service.GetPermisos(rol, user, per);

        }




    }
}
