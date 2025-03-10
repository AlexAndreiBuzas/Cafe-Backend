using CafeBackend.DTOs.Requests;
using CafeBackend.DTOs.Responses;
using CafeBackend.Models;
using CafeBackend.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CafeBackend.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "All fields are required!" });
        }

        var existingUser = await authService.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return Conflict(new { message = "Email is already in use!" });
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = request.Password,
            Role = request.Role ?? "Client"
        };

        await authService.RegisterUser(user);

        return CreatedAtAction(nameof(Register), new { message = "User registered successfully!", user.Email });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Email and password are required!" });
        }

        var user = await authService.Authenticate(request.Email, request.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid email or password!" });
        }

        var token = authService.GenerateJwtToken(user);

        return Ok(new AuthResponse
        {
            Token = token,
            Role = user.Role
        });
    }
}