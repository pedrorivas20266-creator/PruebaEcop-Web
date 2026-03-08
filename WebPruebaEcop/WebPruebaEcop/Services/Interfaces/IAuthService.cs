using RazorPagesApp.Models;

namespace RazorPagesApp.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}