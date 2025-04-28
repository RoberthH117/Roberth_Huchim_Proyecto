using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.ForgetUsuario;
using QualfixAdmin.DataModel.LicenciaUI;
using QualfixAdmin.DataModel.LoginUsuario;
using QualfixAdmin.DataModel.Seguridad;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using QualfixAdmin.model.EntitiSecurity;
using QualfixAdmin.Services.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace QualfixAdmin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LicenciaController : ControllerBase
    {
        private readonly Encripts _service;
        private readonly string secretkey;
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<DataUsuario> _logger;
        private readonly QualfixAdminSecurityContext _context;
        private readonly QualfixAdminContext _db;


        public LicenciaController(Encripts service, SignInManager<User> signInManager, IConfiguration config, UserManager<User> userManager, ILogger<DataUsuario> logger, QualfixAdminSecurityContext context, QualfixAdminContext db) {
            _service=service;
            _signInManager = signInManager;
            _configuration = config;
            secretkey = config.GetSection("settings").GetSection("secretkey").ToString();
            _userManager = userManager;
            _context = context;
            _db=db;

        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("EstadoLicencia")]
        public async Task<IActionResult> GetEstadoLicencia()
        {

            try
            {

                var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
                var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.




                return new OkObjectResult(await _service.EstadoLicenciaTrue(user));

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
        [HttpPost]
        [Route("GenerarLicencia")]
        public async Task<IActionResult> InsertLicencia([FromBody] LicenciaModel request)
        {
            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.
            LicenciaUsuario licenciausuario;
            int EstadoLicencia;

            User userLicencia = null;


            if (request.Id_Usuario != null)
            {

                 EstadoLicencia = await _service.EstadoLicencia(request.Id_Usuario);
            }


         

            


            if (request.Id_Usuario != "")
            {



                userLicencia = await _userManager.FindByIdAsync(request.Id_Usuario);
            }


            else
            {
                userLicencia = await _userManager.FindByIdAsync(user.Id);

            }
        


            var tipoLicencia = _db.TipoDeLicencia.FirstOrDefault(r => r.TipoLicenciaId == request.TipoId);

            if (request.Id_Usuario != "")
            {
                 licenciausuario = _context.LicenciaUsuario.FirstOrDefault(r => r.UserId == request.Id_Usuario);

            }
            else
            {
                 licenciausuario = _context.LicenciaUsuario.FirstOrDefault(r => r.UserId == user.Id);

            }

            if (licenciausuario != null)
            {

                if (request.Id_Usuario == "")
                {
                    request.Id_Usuario = licenciausuario.UserId;
                }

                string clave = _service.GenerateRandomString();
                string LicenciaEncriptada = _service.EncryptString(tipoLicencia.Llave, clave);

                DateTime FechaExp = _service.AddDays(tipoLicencia.Tiempo, DateTime.Now);

                var model = new Licencias
                {
                    Licencia = LicenciaEncriptada,

                    Fecha_Inicio = DateTime.Now,
                    Fecha_Expiracion = FechaExp,
                    Creado_Por = user.Email,
                    UserId = request.Id_Usuario
                };


                var guardar = _context.Licencias.Add(model);

                await _context.SaveChangesAsync();

                var Licencia = await _context.Licencias
        .Include(l => l.LicenciaUsuario)
        .FirstOrDefaultAsync(r => r.Licencia == model.Licencia);



                // Eliminar la entidad existente si existe
                var existingLicenciaUsuario = await _context.LicenciaUsuario.FirstOrDefaultAsync(x => x.UserId == request.Id_Usuario);
                if (existingLicenciaUsuario != null)
                {
                    _context.LicenciaUsuario.Remove(existingLicenciaUsuario);
                    await _context.SaveChangesAsync();
                }

                // Agregar la nueva entidad LicenciaUsuario
                var modelo_usuario = new LicenciaUsuario
                {
                    LicenciaId = Licencia.LicenciaId,
                    UserId = userLicencia.Id
                };

                _context.LicenciaUsuario.Add(modelo_usuario);
                await _context.SaveChangesAsync();



                return StatusCode(StatusCodes.Status200OK, new { status = StatusCodes.Status200OK, Licencia = model.Licencia });



            }

            else {






                string clave = _service.GenerateRandomString();
                string LicenciaEncriptada = _service.EncryptString(tipoLicencia.Llave, clave);



                DateTime FechaExp = _service.AddDays(tipoLicencia.Tiempo, DateTime.Now);

                if (request.Id_Usuario == "")
                {
                    request.Id_Usuario = user.Id;
                    
                }


                var model = new Licencias
                {
                    Licencia = LicenciaEncriptada,

                    Fecha_Inicio = DateTime.Now,
                    Fecha_Expiracion = FechaExp,
                    Creado_Por = user.Email,
                    UserId = request.Id_Usuario
                };


                var guardar = _context.Licencias.Add(model);

                await _context.SaveChangesAsync();

                var Licencia = await _context.Licencias
        .Include(l => l.LicenciaUsuario)
        .FirstOrDefaultAsync(r => r.Licencia == model.Licencia);

                var modeloUsuario = new LicenciaUsuario
                {
                    LicenciaId = Licencia.LicenciaId,
                    UserId = userLicencia.Id
                };

                var asignarLicencia = _context.LicenciaUsuario.Add(modeloUsuario);
                await _context.SaveChangesAsync();





                return StatusCode(StatusCodes.Status200OK, new { status = StatusCodes.Status200OK, Licencia = model.Licencia });

            }
        
        }



        [HttpGet]
        [Route("ObtenerLicencias")]
        public async Task<IActionResult> GetLicencias()
        {

            ForgetUsuario nu = new ForgetUsuario();

            try
            {






                return new OkObjectResult(await _service.GetAllUserLicenses());

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
        [Route("GetLicenciaUsuario")]
        public async Task<IActionResult> GetLicenciaUsuario()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {






                return new OkObjectResult(await _service.ObtenerLicenciasPorUsuarioId(user.Id));

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
        [Route("GetLicencia")]
        public async Task<IActionResult> GetLicencia()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {






                return new OkObjectResult(await _service.usuarioLicencia(user.Id));

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
        [Route("GetUsuario")]
        public async Task<IActionResult> GetUsuario()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.

            try
            {






                return new OkObjectResult(await _service.GetUserLicenseInfo(user.Id));

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
            [Route("GetUsuariosEmpresa")]
            public async Task<IActionResult> GetUsuariosEmpresa()
            {



                try
                {






                    return new OkObjectResult(await _service.GetUsuariosRolAdministradorEmpresa());

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
