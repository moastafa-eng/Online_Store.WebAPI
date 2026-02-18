using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions;
using Shard;
using Shard.DTOs.Products;
using Shard.ModelErrors;

namespace Presentation
{
    [ApiController] // Makes controller handle validation and errors automatically
    [Route("asp/[controller]")] // Defines URL with controller's name dynamically
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase  // ControllerBase does not contain View Tools
    {
        [HttpGet] // Route : BaseUrl/api/products
        [Cache(60)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProductsAsync([FromQuery] ProductQueryParameters parameters) // FromQuery : parameters come from UrlQuery (Query params after (?))
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(result);
        }



        [HttpGet("{id}")] // Route : BaseUrl/api/products/2
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResponse>> GetProductById(int? id)
        {
            // if id is null return status code 400 BadRequest.
            if (id is null) return BadRequest();

            var product = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);

            return Ok(product);
        }



        [HttpGet("brands")] // Route : BaseUrl/api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();

            if (result is null) return NotFound();

            return Ok(result);
        }



        [HttpGet("Types")] // Route : BaseUrl/api/products/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAllTypesAsync()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();

            if (result is null) return NotFound();

            return Ok(result);
        }
    }
}
