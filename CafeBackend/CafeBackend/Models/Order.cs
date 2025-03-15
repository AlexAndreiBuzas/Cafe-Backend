namespace CafeBackend.Models;

public class Order
{
    public int Id { get; set; }
    
    public int UserId { get; set; }  
    
    public string PickupCode { get; set; } = GeneratePickupCode();
    
    public string CoffeeType { get; set; } = string.Empty; 
    
    public string Size { get; set; } = string.Empty;  
    
    public string Customizations { get; set; } = string.Empty;
    
    public string Status { get; set; } = "Pending";  
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    
    public User? User { get; set; }
    
    private static string GeneratePickupCode()
    {
        return new Random().Next(1000, 9999).ToString();
    }
}