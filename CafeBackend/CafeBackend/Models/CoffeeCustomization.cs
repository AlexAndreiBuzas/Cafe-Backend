namespace CafeBackend.Models;

public class CoffeeCustomization
{
    public int Id { get; set; }
    
    public string Category { get; set; } = string.Empty;
    
    public string Option { get; set; } = string.Empty;
    
    public decimal AdditionalCost { get; set; }
}