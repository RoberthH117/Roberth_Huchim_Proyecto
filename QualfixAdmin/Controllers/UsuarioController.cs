using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using QualfixAdmin.Services.CreacionRol;
using QualfixAdmin.Services.Login;
using QualfixAdmin.Services.Perfil_Ser;
using QualfixAdmin.Services.Usuario;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly RegistroUsuarioServices _usuarioService;
        private readonly UserManager<User> _userManager;
        private readonly PerfilServicio _perfilServicio;
        private readonly RolService _rolService;
        private readonly Login _loginService;
        private readonly QualfixAdminContext _bd;
        private readonly QualfixAdminSecurityContext _context;

        public  UsuarioController (RegistroUsuarioServices usuarioService, UserManager<User> userManager, RolService rolService, PerfilServicio perfilServicio, Login loginService, QualfixAdminContext bd,QualfixAdminSecurityContext context)
        {
            _usuarioService = usuarioService;
            _userManager = userManager;
            _rolService=rolService;
            _perfilServicio = perfilServicio;
            _loginService = loginService;
            _bd = bd;
            _context = context;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("ObtenerUsuario")]
        public async Task<IActionResult> ObtenerTodosUsuarios()
        {

            var idUsuario = User.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
            var user = await _userManager.FindByIdAsync(idUsuario); // Obtiene el usuario actual.
            try {
                var resul = await _usuarioService.GetUsuariosConPerfiles(user);

                return new JsonResult (resul);
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(string id)
        {
            try { 
                var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return new JsonResult(usuario);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AgregarUsuario")]
        public async Task<IActionResult> AgregarUsuario([FromBody] BodyRequest usuario)
        {
            try
            {
                User user = usuario.usuario;

                if (usuario.nombreCompany != null)
                {

                    var compa = _bd.Clientes.FirstOrDefault(r => r.NombreComercial == usuario.nombreCompany);
                    user.Id_Company = compa.ClienteId;
                }


               

                var resultadoCreacion = await _loginService.AgregarUsuario(usuario);

 //_usuarioService.EnviarCorreoValidacion(user.FirstName);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

       


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(string id, [FromBody] User usuario)
        {
            try
            {
                var existingUsuario = await _usuarioService.ObtenerUsuarioPorId(id);

                if (existingUsuario == null)
                {
                    return NotFound();
                }

                usuario.Id = id;
               await _usuarioService.ActualizarUsuario(usuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("El ID proporcionado no es válido.");
                }

                var usuario = await _usuarioService.ObtenerUsuarioPorId(id);

                if (usuario == null)
                {
                    return NotFound();
                }

               await _usuarioService.EliminarUsuario(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }
    }
}
