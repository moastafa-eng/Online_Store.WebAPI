using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shard.DTOs.Orders;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost] // Url : BaseUrr/api/Orders
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync(OrderRequest request)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.CreateOrderAsync(request, userEmailClaim!.Value);
            return Ok(result);
        }
    }
}
