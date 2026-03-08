using RazorPagesApp.Models;

namespace RazorPagesApp.Services.Interfaces;

public interface IClienteService
{
    Task<List<Cliente>> GetClientesAsync(string? busqueda = null, int? limite = null);
    Task<Cliente?> GetClienteByIdAsync(int id);
    Task<Cliente?> CreateClienteAsync(Cliente cliente);
    Task<Cliente?> UpdateClienteAsync(int id, Cliente cliente);
    Task<bool> DeleteClienteAsync(int id);
    Task<List<Cliente>> GetClientesActivosAsync();
}