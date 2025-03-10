namespace CafeBackend.DTOs.Responses;

public class ReservationResponse
{
    public int Id { get; set; }
    public DateTime ReservationTime { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
}