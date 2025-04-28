
//using QualfixAdmin.dal.dbContext;
using QualfixAdmin.model;
using QualfixAdmin.Services.Login;
using QualfixAdmin.DataModel.LoginUsuario;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using QualfixAdmin.DataModel.ForgetUsuario;
using Microsoft.AspNetCore.Identity;
using QualfixAdmin.DataModel.UsuarioModel;
using Microsoft.AspNetCore.Mvc;

using QualfixAdmin.DataModel.Seguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QualfixAdmin.dal.DBContext;
using Microsoft.EntityFrameworkCore;

namespace QualfixAdmin.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController : Controller
    {

        private readonly Login _service;
        private readonly string secretkey;
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<DataUsuario> _logger;
        private readonly QualfixAdminSecurityContext _context;


        public LoginController(Login service, SignInManager<User> signInManager, IConfiguration config, UserManager<User> userManager,  ILogger<DataUsuario> logger, QualfixAdminSecurityContext context)
        {

            _service = service;
            _signInManager = signInManager;
            _configuration = config;
            secretkey = config.GetSection("settings").GetSection("secretkey").ToString();
            _userManager = userManager;
            _context= context;
          
        }

      
        [HttpPost]
        [Route("Validar")]
        public async Task<IActionResult> Validar([FromBody] LoginUsuario request)
        {
            // Realiza la autenticación aquí (por ejemplo, verificando el usuario y contraseña)

            var user = await _userManager.FindByEmailAsync(request.correo);



            var usuarioPerfil = await _context.UsuariosPerfiles
          .Include(up => up.Perfil)  // Incluye la entidad Perfil en la consulta
          .FirstOrDefaultAsync(up => up.UserId == user.Id);



            if (user != null && await _userManager.CheckPasswordAsync(user, request.passwordHash))
            {



                // Obtiene los roles del usuario
                var userRoles = await _userManager.GetRolesAsync(user);

                // El usuario existe y la contraseña es válida
                var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id), // Aquí asigna el ID del usuario
    new Claim(ClaimTypes.Name, user.Email), // Aquí asigna el correo electrónico del usuario
   new Claim("PerfilId", usuarioPerfil.PerfilId.ToString()),
 
    // Puedes agregar otros claims según tus necesidades
};


                // Agrega los roles como claims
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }


                var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1), // Duración del token
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Devuelve el token como parte de la respuesta
                // return Ok(new { Token = tokenString });

                return StatusCode(StatusCodes.Status200OK, new { status = StatusCodes.Status200OK, token = tokenString });
            }

            // Si el usuario o la contraseña son incorrectos, muestra un error
            return StatusCode(StatusCodes.Status200OK, new { status = StatusCodes.Status401Unauthorized, token=""});
            //return Unauthorized();
        }



        [HttpPost]
        [Route("Forget")]
    
        public async Task<IActionResult> Forget([FromBody] ForgetUsuario request)
        {

            ForgetUsuario nu = new ForgetUsuario();

            try
            {
                var data = await _service.GetUsuario();





                foreach (var item in data.DataResponse)
                {
                    if (request.correo == item.Email)
                    {
                        nu.id = item.Id;
                        nu.correo = item.Email;

                        //nu.contrasenia = item.Contrasenia;
                        //nu.status = item.Estado;


                    }






                }

                if (nu.correo == request.correo && nu.id == request.id)
                {

                

                    return new OkObjectResult(await _service.ForgetUser(request));

                }
                else
                {

                   
                    return new OkObjectResult(await _service.ForgetUser());
                   
                }



               
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
        //[Route("Registro")]
        //public async Task<IActionResult> Registro([FromBody] User user)
        //{

        //    ForgetUsuario nu = new ForgetUsuario();

        //    try
        //    {
               





        //        return new OkObjectResult(await _service.AgregarUsuario(user));

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










        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Comprobar")]
        public async Task<bool> Comprobar()
        {



            return true;

        }





    }
}