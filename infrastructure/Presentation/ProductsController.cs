using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation
{
    [ApiController] // Makes controller handle validation and errors automatically
    [Route("asp/[controller]")] // Defines URL with controller's name dynamically
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase  // ControllerBase does not contain View Tools
    {
        [HttpGet] // Route : BaseUrl/api/products
        public async Task<IActionResult> GetAllProductsAsync(int? brandId, int? typeId, string? sort, string? search, int? pageIndex = 3, int? pageSize = 2)
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(brandId, typeId, sort, search, pageIndex, pageSize);

            // if result == null return status code 404 not found else return status code 200 Successfully 
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")] // Route : BaseUrl/api/products/2
        public async Task<IActionResult> GetProductById(int? id)
        {
            // if id is null return status code 400 BadRequest.
            if (id is null) return BadRequest();

            var product = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);

            if (product is null) return NotFound(); 

            return Ok(product);
        }

        [HttpGet("brands")] // Route : BaseUrl/api/products/brands
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost("Types")] // Route : BaseUrl/api/products/types
        public async Task<IActionResult> GetAllTypesAsync()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();

            if (result is null) return NotFound();

            return Ok(result);
        }
    }
}
