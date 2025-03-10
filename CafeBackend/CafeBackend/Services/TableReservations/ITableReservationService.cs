using CafeBackend.DTOs.Requests;
using CafeBackend.Models;

namespace CafeBackend.Services.TableReservations;

public interface ITableReservationService
{
    Task<TableReservation?> GetReservationByIdAsync(int id);
    Task<List<TableReservation>> GetReservationsByUserIdAsync(int userId);
    Task<TableReservation> CreateReservationAsync(int userId, CreateReservationRequest request);
    Task<bool> UpdateReservationStatusAsync(int reservationId, string newStatus);
    Task<List<TableReservation>> GetAllReservationsAsync();
}