using CafeBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Controllers;

[Route("api/coffee-options")]
[ApiController]
public class CoffeeOptionsController(AppDbContext context) : ControllerBase
{
    [HttpGet("types")]
    public async Task<IActionResult> GetCoffeeTypes()
    {
        var coffeeTypes = await context.CoffeeTypes.ToListAsync();
        return Ok(coffeeTypes);
    }

    [HttpGet("customizations")]
    public async Task<IActionResult> GetCustomizations()
    {
        var customizations = await context.CoffeeCustomizations.ToListAsync();
        return Ok(customizations);
    }
}