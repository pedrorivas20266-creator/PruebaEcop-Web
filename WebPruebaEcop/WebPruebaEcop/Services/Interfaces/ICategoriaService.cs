using RazorPagesApp.Models;

namespace RazorPagesApp.Services.Interfaces;

public interface ICategoriaService
{
    Task<List<Categoria>> GetCategoriasAsync();
}