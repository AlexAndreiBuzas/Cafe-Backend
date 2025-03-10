using CafeBackend.Services.Orders;
using CafeBackend.Services.TableReservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeBackend.Controllers;

[Route("api/dashboard")]
[ApiController]
[Authorize(Roles = "Barista")]
public class BaristaDashboardController(IOrderService orderService, ITableReservationService reservationService)
    : ControllerBase
{
    [HttpGet("orders")]
    public async Task<IActionResult> GetActiveOrders()
    {
        var orders = await orderService.GetAllOrdersAsync();
        var activeOrders = orders.Where(o => o.Status is "Pending" or "In Progress").ToList();
        return Ok(activeOrders);
    }

    [HttpGet("reservations")]
    public async Task<IActionResult> GetPendingReservations()
    {
        var reservations = await reservationService.GetAllReservationsAsync();
        var pendingReservations = reservations.Where(r => r.Status == "Pending").ToList();
        return Ok(pendingReservations);
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetDashboardSummary()
    {
        var orders = await orderService.GetAllOrdersAsync();
        var reservations = await reservationService.GetAllReservationsAsync();

        var summary = new
        {
            TotalOrders = orders.Count,
            ActiveOrders = orders.Count(o => o.Status is "Pending" or "In Progress"),
            CompletedOrders = orders.Count(o => o.Status == "Completed"),
            TotalReservations = reservations.Count,
            PendingReservations = reservations.Count(r => r.Status == "Pending")
        };

        return Ok(summary);
    }
}