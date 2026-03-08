using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;
using System.Net.Http.Json;

namespace RazorPagesApp.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/login", request);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            
            return null;
        }
        catch
        {
            return null;
        }
    }
}