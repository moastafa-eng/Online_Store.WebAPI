using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shard.DTOs.Baskets;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet("{id}")] // Rout => BaseUrl/api/baskets?id=1
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost] // Rout => baseUrl/api/baskets
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
            var result = await _serviceManager.BasketService.CreateBasketAsync(dto, TimeSpan.FromDays(1));
            return Ok(result);
        }

        [HttpDelete] // Rout => baseUrl/api/baskets?id
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteBasket(id);
         
            return NoContent(); // successful operation with status code 204
        }
    }
}
