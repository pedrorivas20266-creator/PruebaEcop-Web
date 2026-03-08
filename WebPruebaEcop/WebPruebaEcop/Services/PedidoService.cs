using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;

namespace RazorPagesApp.Services;

public class PedidoService : IPedidoService
{
    private readonly HttpClient _httpClient;

    public PedidoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResponse<PedidoResponse>> GetPedidosAsync(
        string? busqueda = null,
        DateTime? fechaDesde = null,
        DateTime? fechaHasta = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = $"/api/pedidos?busqueda={busqueda}" +
                   $"&fechaDesde={fechaDesde:yyyy-MM-dd}" +
                   $"&fechaHasta={fechaHasta:yyyy-MM-dd}" +
                   $"&pageNumber={pageNumber}&pageSize={pageSize}";

        return await _httpClient.GetFromJsonAsync<PaginatedResponse<PedidoResponse>>(query)
            ?? new PaginatedResponse<PedidoResponse>();
    }

    public async Task<PedidoResponse?> GetPedidoByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PedidoResponse>($"/api/pedidos/{id}");
    }

    public async Task<PedidoResponse?> CreatePedidoAsync(PedidoRequest pedido)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/pedidos", pedido);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error API: {errorContent}");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<PedidoResponse>();
    }

    public async Task<PedidoResponse?> AnularPedidoAsync(int id, string motivo)
    {
        var request = new { MotivoAnulacion = motivo };
        var response = await _httpClient.PutAsJsonAsync($"/api/pedidos/{id}/anular", request);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PedidoResponse>()
            : null;
    }
}