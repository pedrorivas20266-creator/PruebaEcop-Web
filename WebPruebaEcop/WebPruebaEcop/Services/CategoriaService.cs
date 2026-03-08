using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;
using System.Net.Http.Json;

namespace RazorPagesApp.Services;

public class CategoriaService : ICategoriaService
{
    private readonly HttpClient _httpClient;

    public CategoriaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Categoria>> GetCategoriasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Categoria>>("/api/categorias") 
            ?? new List<Categoria>();
    }
}