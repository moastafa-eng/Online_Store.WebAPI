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
        [HttpPost] // Post, Url : BaseUrr/api/Orders
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync(OrderRequest request)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);

            var orderResponse = await _serviceManager.OrderService.CreateOrderAsync(request, userEmailClaim!.Value);
            return Ok(orderResponse);
        }

        [HttpGet("deliveryMethods")] // Get, Url : / BaseUrl/api/Orders/deliveryMethod
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var deliveryMethodResponse = await _serviceManager.OrderService.GetAllDeliveryMethodsAsync();

            return Ok(deliveryMethodResponse);
        }

        [HttpGet("{id}")] // Get, Url : BaseUrl/api/orders?id
        [Authorize]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email); 

            var orderResponse = await _serviceManager.OrderService.GetOrderByIdForSpecificUserAsync(id, userEmail!.Value);
            return Ok(orderResponse);
        }

        [HttpGet()] // Get, Url : BaseUrl/api/Orders
        [Authorize]
        public async Task<IActionResult> GetOrdersByIdForSpecificUser()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);

            var orderResponse = await _serviceManager.OrderService.GetOrdersForSpecificUserAsync(userEmail!.Value);
            return Ok(orderResponse);
        }
    }
}
