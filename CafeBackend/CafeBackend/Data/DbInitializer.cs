using CafeBackend.Models;

namespace CafeBackend.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (!context.CoffeeTypes.Any())
        {
            context.CoffeeTypes.AddRange(new List<CoffeeType>
            {
                new CoffeeType { Name = "Espresso", Description = "A strong black coffee", Price = 2.50m },
                new CoffeeType { Name = "Latte", Description = "Espresso with steamed milk", Price = 3.50m },
                new CoffeeType { Name = "Cappuccino", Description = "Espresso with milk and foam", Price = 3.75m }
            });
        }

        if (!context.CoffeeCustomizations.Any())
        {
            context.CoffeeCustomizations.AddRange(new List<CoffeeCustomization>
            {
                new CoffeeCustomization { Category = "Milk Type", Option = "Oat Milk", AdditionalCost = 0.50m },
                new CoffeeCustomization { Category = "Milk Type", Option = "Almond Milk", AdditionalCost = 0.50m },
                new CoffeeCustomization { Category = "Extra Shots", Option = "1 Extra Shot", AdditionalCost = 1.00m },
                new CoffeeCustomization { Category = "Extra Shots", Option = "2 Extra Shots", AdditionalCost = 1.50m },
                new CoffeeCustomization { Category = "Syrup", Option = "Vanilla Syrup", AdditionalCost = 0.75m },
                new CoffeeCustomization { Category = "Syrup", Option = "Caramel Syrup", AdditionalCost = 0.75m }
            });
        }

        context.SaveChanges();
    }
}