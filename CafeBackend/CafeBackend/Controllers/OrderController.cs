using System.Security.Claims;
using CafeBackend.DTOs.Requests;
using CafeBackend.DTOs.Responses;
using CafeBackend.Services.Auth;
using CafeBackend.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeBackend.Controllers;

[Route("api/orders")][ApiController]
public class OrderController(IOrderService orderService, IAuthService authService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly IAuthService _authService = authService;
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var order = await _orderService.PlaceOrderAsync(userId, request);

        return CreatedAtAction(nameof(PlaceOrder), new OrderResponse
        {
            Id = order.Id,
            CoffeeType = order.CoffeeType,
            Size = order.Size,
            Customizations = order.Customizations,
            Status = order.Status,
            CreatedAt = order.CreatedAt
        });
    }
    
    [Authorize]
    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var orders = await _orderService.GetOrdersByUserIdAsync(userId);

        return Ok(orders);
    }
    
    [Authorize(Roles = "Barista")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }
    
    [Authorize(Roles = "Barista")]
    [HttpPatch("{orderId}")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
    {
        var updated = await _orderService.UpdateOrderStatusAsync(orderId, newStatus);
        if (!updated) return NotFound(new { message = "Order not found" });

        return Ok(new { message = "Order status updated!" });
    }
}