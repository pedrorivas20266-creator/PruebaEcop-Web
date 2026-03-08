using RazorPagesApp.Models;

namespace RazorPagesApp.Services.Interfaces;

public interface IUnidadMedidaService
{
    Task<List<UnidadMedida>> GetUnidadesMedidaAsync();
}