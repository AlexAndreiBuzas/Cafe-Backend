using CafeBackend.DTOs.Requests;
using CafeBackend.Models;

namespace CafeBackend.Services.Orders;

public interface IOrderService
{
    Task<Order?> GetOrderByIdAsync(int id);
    
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    
    Task<Order> PlaceOrderAsync(int userId, PlaceOrderRequest request);
    
    Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
    
    Task<List<Order>> GetAllOrdersAsync(); 
    
    Task<Order?> GetOrderByPickupCodeAsync(string pickupCode);
}