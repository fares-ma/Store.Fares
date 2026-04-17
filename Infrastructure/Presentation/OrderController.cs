using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost] // POST: /api/Orders
        public async Task<IActionResult> CreateOrder(OrderRequestDto request)
        {
          var email = User.FindFirstValue(ClaimTypes.Email);

         var result = await serviceManager.OrderService.CreateOrderAsync(request, email);

            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.GetOrderByUserEmailAsync(email);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(Guid id)
        {
          

            var result = await serviceManager.OrderService.GetOrderByIdAsync(id);

            return Ok(result);
        }


        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
          

            var result = await serviceManager.OrderService.GetAllDeliveryMethods();

            return Ok(result);
        }

    }
}
