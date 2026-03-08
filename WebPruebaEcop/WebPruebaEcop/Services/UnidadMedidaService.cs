using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;
using System.Net.Http.Json;

namespace RazorPagesApp.Services;

public class UnidadMedidaService : IUnidadMedidaService
{
    private readonly HttpClient _httpClient;

    public UnidadMedidaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UnidadMedida>> GetUnidadesMedidaAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UnidadMedida>>("/api/unidades-medida") 
            ?? new List<UnidadMedida>();
    }
}