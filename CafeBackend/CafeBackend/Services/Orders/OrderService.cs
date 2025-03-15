using CafeBackend.Data;
using CafeBackend.DTOs.Requests;
using CafeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Services.Orders;

public class OrderService(AppDbContext context) : IOrderService
{
    private readonly AppDbContext _context = context;

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Order> PlaceOrderAsync(int userId, PlaceOrderRequest request)
    {
        var order = new Order
        {
            UserId = userId,
            CoffeeType = request.CoffeeType,
            Size = request.Size,
            Customizations = request.Customizations ?? "",
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;

        order.Status = newStatus;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<Order?> GetOrderByPickupCodeAsync(string pickupCode)
    {
        return await _context.Orders
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.PickupCode == pickupCode);
    }
}