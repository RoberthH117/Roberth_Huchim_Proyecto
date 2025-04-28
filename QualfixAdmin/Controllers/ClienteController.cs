using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.model.QualfixAdminData;
using QualfixAdmin.Services.ClienteService;

namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteServices)
        {
            _clienteService = clienteServices;
        }

        [HttpGet]
        public IActionResult ObtenerTodosClientes()
        {
            try
            {
                var usuarios = _clienteService.ObtenerTodosClientes();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerClientePorId(int id)
        {
            try
            {
                var usuario = _clienteService.ObtenerClientePorId(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AgregarCliente([FromBody] Cliente cliente)
        {
            try
            {
                _clienteService.AgregarCliente(cliente);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            try
            {
                var existingUsuario = _clienteService.ObtenerClientePorId(id);

                if (existingUsuario == null)
                {
                    return NotFound();
                }

                cliente.ClienteId = id;
                _clienteService.ActualizarCliente(cliente);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCliente(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("El ID proporcionado no es válido.");
                }

                var usuario = _clienteService.ObtenerClientePorId(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _clienteService.EliminarCliente(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }
    
}
}
