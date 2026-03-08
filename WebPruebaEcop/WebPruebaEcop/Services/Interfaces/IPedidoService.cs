using RazorPagesApp.Models;

namespace RazorPagesApp.Services.Interfaces;

public interface IPedidoService
{
    Task<PaginatedResponse<PedidoResponse>> GetPedidosAsync(
        string? busqueda = null, 
        DateTime? fechaDesde = null, 
        DateTime? fechaHasta = null, 
        int pageNumber = 1, 
        int pageSize = 10);
    Task<PedidoResponse?> GetPedidoByIdAsync(int id);
    Task<PedidoResponse?> CreatePedidoAsync(PedidoRequest pedido);
    Task<PedidoResponse?> AnularPedidoAsync(int id, string motivo);
}