using RazorPagesApp.Models;

namespace RazorPagesApp.Services.Interfaces;

public interface IProductoService
{
    Task<List<ProductoQuery>> GetProductosAsync(string? busqueda = null, int? limite = null);
    Task<Producto?> GetProductoByIdAsync(int id);
    Task<ProductoConPrecioResponse?> CreateProductoAsync(ProductoRequest producto);
    Task<ProductoQuery?> UpdateProductoAsync(int id, ProductoQuery producto);
    Task<bool> DeleteProductoAsync(int id);
    Task<List<Producto>> GetProductosActivosAsync();
    Task<ProductoConPrecioResponse?> GetProductoConPreciosAsync(int id);
}