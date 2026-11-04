using GestionProductos.BusinessLayer.Services;
using GestionProductos.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionProductos.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _service.GetAllAsync();
            return Ok(productos);
        }

        // GET: api/productos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _service.GetByIdAsync(id);
            if (producto == null) return NotFound();

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nuevoProducto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var actualizado = await _service.UpdateAsync(id, dto);
            if (!actualizado) return NotFound();

            return NoContent();
        }

        // DELETE: api/productos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
