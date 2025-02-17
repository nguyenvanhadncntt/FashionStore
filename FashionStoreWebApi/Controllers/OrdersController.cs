using System.Security.Claims;
using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Models.Enumerations;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrderAsync([FromBody] OrderVm orderVm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderService.PlaceOrderAsync(userId, orderVm);
            return Ok();
        }


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailAsync(long orderId)
        {
            var orderVm = await _orderService.GetOrderDetailAsync(orderId);
            return Ok(orderVm);
        }

        [HttpGet]
        [Route("my-orders")]
        public async Task<IActionResult> GetMyOrdersAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetMyOrdersAsync(userId);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] PagingRequest pagingRequest)
        {
            var orders = await _orderService.GetOrdersAsync(pagingRequest);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("update-status/{orderId}/{status}")]
        public async Task<IActionResult> UpdateOrderStatusAsync(long orderId, OrderStatus status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderAsync(long orderId)
        {
            await _orderService.DeleteOrderAsync(orderId);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("update-payment-status/{orderId}/{status}")]
        public async Task<IActionResult> UpdatePaymentStatusAsync(long orderId, PaymentStatus status)
        {
            await _orderService.UpdatePaymentStatusAsync(orderId, status);
            return Ok();
        }
    }
}
