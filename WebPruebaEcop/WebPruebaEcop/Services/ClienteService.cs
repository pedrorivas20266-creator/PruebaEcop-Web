using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;

namespace RazorPagesApp.Services;

public class ClienteService : IClienteService
{
    private readonly HttpClient _httpClient;

    public ClienteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Cliente>> GetClientesAsync(string? busqueda = null, int? limite = null)
    {
        var query = $"/api/clientes?busqueda={busqueda}&limite={limite}";
        return await _httpClient.GetFromJsonAsync<List<Cliente>>(query) ?? new List<Cliente>();
    }

    public async Task<Cliente?> GetClienteByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Cliente>($"/api/clientes/{id}");
    }

    public async Task<Cliente?> CreateClienteAsync(Cliente cliente)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/clientes", cliente);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<Cliente>()
            : null;
    }

    public async Task<Cliente?> UpdateClienteAsync(int id, Cliente cliente)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/clientes/{id}", cliente);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<Cliente>()
            : null;
    }

    public async Task<bool> DeleteClienteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/clientes/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<Cliente>> GetClientesActivosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Cliente>>("/api/clientes/activos")
            ?? new List<Cliente>();
    }
}