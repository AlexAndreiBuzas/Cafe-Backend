namespace CafeBackend.Models;

public class TableReservation
{
    public int Id { get; set; }
    
    public int UserId { get; set; } 
    
    private DateTime _reservationTime;
    
    public DateTime ReservationTime 
    { 
        get => _reservationTime;
        set => _reservationTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
    }

    private DateTime _createdAt = DateTime.UtcNow;
    
    public DateTime CreatedAt
    { 
        get => _createdAt;
        set => _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    
    public string Status { get; set; } = "Pending";

    public User? User { get; set; }
}