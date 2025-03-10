using CafeBackend.Models;

namespace CafeBackend.Services.Auth;

public interface IAuthService
{
    Task<User?> Authenticate(string email, string password);
    
    Task RegisterUser(User user);
    
    string GenerateJwtToken(User user);

    Task<User?> GetByEmailAsync(string email);
}