namespace CafeBackend.DTOs.Requests;

public class PlaceOrderRequest
{
    public required string CoffeeType { get; set; }
    public required string Size { get; set; }
    public string? Customizations { get; set; }
}