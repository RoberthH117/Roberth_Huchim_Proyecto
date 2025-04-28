using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualfixAdmin.model;
using QualfixAdmin.model.QualfixAdminData;
using QualfixAdmin.Services.Producto;
namespace QualfixAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProductoController : ControllerBase
    {
        private readonly ProductoServices _productoService;

        public ProductoController(ProductoServices productoServices)
        {
            _productoService = productoServices;
        }

        [HttpGet]
        public IActionResult ObtenerTodosProductos(int pagina)
        {
            try
            {
                var usuarios = _productoService.ObtenerTodosProductos(pagina);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerProductosPorId(int id)
        {
            try
            {
                var usuario = _productoService.ObtenerproductoPorId(id);
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
        public IActionResult AgregarProductos([FromBody] Productos producto)
        {
            try
            {
                _productoService.AgregarProducto(producto);
               
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public IActionResult ActualizarProductos(int id, [FromBody] Productos producto)
        {
            try
            {
                var existingUsuario = _productoService.ObtenerproductoPorId(id);

                if (existingUsuario == null)
                {
                    return NotFound();
                }

                producto.Id = id;
                _productoService.ActualizarProducto(producto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProductos(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("El ID proporcionado no es válido.");
                }

                var usuario = _productoService.ObtenerproductoPorId(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _productoService.Eliminarproducto(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error interno: {ex.Message}");
            }
        }
    }
}
