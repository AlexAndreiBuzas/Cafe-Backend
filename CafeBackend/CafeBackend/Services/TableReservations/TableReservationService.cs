using CafeBackend.Data;
using CafeBackend.DTOs.Requests;
using CafeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Services.TableReservations;

public class TableReservationService(AppDbContext context) : ITableReservationService
{
    private readonly AppDbContext _context = context;

    public async Task<TableReservation?> GetReservationByIdAsync(int id)
    {
        return await _context.TableReservations.FindAsync(id);
    }

    public async Task<List<TableReservation>> GetReservationsByUserIdAsync(int userId)
    {
        return await _context.TableReservations
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TableReservation> CreateReservationAsync(int userId, CreateReservationRequest request)
    {
        var reservation = new TableReservation
        {
            UserId = userId,
            ReservationTime = request.ReservationTime,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.TableReservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<bool> UpdateReservationStatusAsync(int reservationId, string newStatus)
    {
        var reservation = await _context.TableReservations.FindAsync(reservationId);
        if (reservation == null) return false;

        reservation.Status = newStatus;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TableReservation>> GetAllReservationsAsync()
    {
        return await _context.TableReservations
            .OrderByDescending(r => r.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }
}