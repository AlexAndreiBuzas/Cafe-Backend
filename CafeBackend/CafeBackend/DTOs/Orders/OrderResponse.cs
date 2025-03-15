namespace CafeBackend.DTOs.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    
    
    public string CoffeeType { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Customizations { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public string PickupCode { get; set; }
}