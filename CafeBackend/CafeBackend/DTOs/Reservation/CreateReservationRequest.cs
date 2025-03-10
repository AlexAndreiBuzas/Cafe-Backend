namespace CafeBackend.DTOs.Requests;

public class CreateReservationRequest
{
    public required DateTime ReservationTime { get; set; }
}