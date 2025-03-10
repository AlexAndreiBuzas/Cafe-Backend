using System.Security.Claims;
using CafeBackend.DTOs.Requests;
using CafeBackend.DTOs.Responses;
using CafeBackend.Services.TableReservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeBackend.Controllers;

[Route("api/reservations")]
[ApiController]
public class TableReservationController(ITableReservationService reservationService) : ControllerBase
{
    private readonly ITableReservationService _reservationService = reservationService;
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var reservation = await _reservationService.CreateReservationAsync(userId, request);

        return CreatedAtAction(nameof(CreateReservation), new ReservationResponse
        {
            Id = reservation.Id,
            ReservationTime = reservation.ReservationTime,
            Status = reservation.Status,
            CreatedAt = reservation.CreatedAt
        });
    }
    
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyReservations()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
        return Ok(reservations);
    }
    
    [Authorize(Roles = "Barista")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        return Ok(reservations);
    }
    
    [Authorize(Roles = "Barista")]
    [HttpPatch("{reservationId:int}")]
    public async Task<IActionResult> UpdateReservationStatus(int reservationId, [FromBody] string newStatus)
    {
        var updated = await _reservationService.UpdateReservationStatusAsync(reservationId, newStatus);
        if (!updated) return NotFound(new { message = "Reservation not found" });

        return Ok(new { message = "Reservation status updated!" });
    }
    
    [Authorize(Roles = "Barista")]
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetReservationsByStatus(string status)
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        var filteredReservations = reservations
            .Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(filteredReservations);
    }

    [Authorize(Roles = "Barista")]
    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingReservations()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        var upcomingReservations = reservations
            .Where(r => r.ReservationTime > DateTime.UtcNow)
            .OrderBy(r => r.ReservationTime)
            .ToList();

        return Ok(upcomingReservations);
    }
}