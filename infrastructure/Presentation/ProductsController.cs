using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation
{
    [ApiController]
    [Route("asp/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync();

            if (result is null) return BadRequest();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);

            if (product is null) return NotFound(); 

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();

            if (result is null) return BadRequest();

            return Ok(result);
        }

        [HttpPost("Types")]
        public async Task<IActionResult> GetAllTypesAsync()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();

            if (result is null) return BadRequest();

            return Ok(result);
        }
    }
}
