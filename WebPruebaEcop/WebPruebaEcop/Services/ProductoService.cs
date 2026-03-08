using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;
using System.Net.Http.Json;

namespace RazorPagesApp.Services;

public class ProductoService : IProductoService
{
    private readonly HttpClient _httpClient;

    public ProductoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductoQuery>> GetProductosAsync(string? busqueda = null, int? limite = null)
    {
        var query = $"/api/productos?busqueda={busqueda}&limite={limite}";
        return await _httpClient.GetFromJsonAsync<List<ProductoQuery>>(query) ?? new List<ProductoQuery>();
    }

    public async Task<Producto?> GetProductoByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Producto>($"/api/productos/{id}");
    }

    public async Task<ProductoConPrecioResponse?> CreateProductoAsync(ProductoRequest producto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/productos", producto);
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<ProductoConPrecioResponse>() 
            : null;
    }

    public async Task<ProductoQuery?> UpdateProductoAsync(int id, ProductoQuery producto)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/productos/{id}", producto);
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<ProductoQuery>() 
            : null;
    }

    public async Task<bool> DeleteProductoAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/productos/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<Producto>> GetProductosActivosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>("/api/productos/activos") 
            ?? new List<Producto>();
    }

    public async Task<ProductoConPrecioResponse?> GetProductoConPreciosAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductoConPrecioResponse>(
            $"/api/productos/{id}/con-precios");
    }
}