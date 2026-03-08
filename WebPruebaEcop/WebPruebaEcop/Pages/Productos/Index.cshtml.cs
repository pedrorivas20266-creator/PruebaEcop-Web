using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;

namespace WebPruebaEcop.Pages.Productos;

public class IndexModel : PageModel
{
    private readonly IProductoService _productoService;
    private readonly ICategoriaService _categoriaService;
    private readonly IUnidadMedidaService _unidadMedidaService;

    public List<ProductoQuery> Productos { get; set; } = new();

    [BindProperty]
    public string? Busqueda { get; set; }

    public IndexModel(
        IProductoService productoService,
        ICategoriaService categoriaService,
        IUnidadMedidaService unidadMedidaService)
    {
        _productoService = productoService;
        _categoriaService = categoriaService;
        _unidadMedidaService = unidadMedidaService;
    }

    public async Task<IActionResult> OnGetAsync(string? busqueda = null)
    {
        var token = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToPage("/Login");
        }

        Busqueda = busqueda;
        Productos = await _productoService.GetProductosAsync(busqueda, 100);
        return Page();
    }

    public async Task<IActionResult> OnGetCategoriasAsync()
    {
        var categorias = await _categoriaService.GetCategoriasAsync();
        return new JsonResult(categorias);
    }

    public async Task<IActionResult> OnGetUnidadesMedidaAsync()
    {
        var unidades = await _unidadMedidaService.GetUnidadesMedidaAsync();
        return new JsonResult(unidades);
    }

    public async Task<IActionResult> OnPostCreateAsync([FromBody] ProductoRequest producto)
    {
        if (producto == null)
        {
            return new JsonResult(new { success = false, message = "Datos inválidos" });
        }

        var result = await _productoService.CreateProductoAsync(producto);
        
        if (result != null)
        {
            return new JsonResult(new { success = true, message = "Producto creado exitosamente", data = result });
        }

        return new JsonResult(new { success = false, message = "Error al crear producto" });
    }

    public async Task<IActionResult> OnPostUpdateAsync([FromBody] ProductoQuery producto, int id)
    {
        if (producto == null)
        {
            return new JsonResult(new { success = false, message = "Datos inválidos" });
        }

        var result = await _productoService.UpdateProductoAsync(id, producto);
        
        if (result != null)
        {
            return new JsonResult(new { success = true, message = "Producto actualizado exitosamente", data = result });
        }

        return new JsonResult(new { success = false, message = "Error al actualizar producto" });
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var result = await _productoService.DeleteProductoAsync(id);
        
        if (result)
        {
            return new JsonResult(new { success = true, message = "Producto eliminado exitosamente" });
        }

        return new JsonResult(new { success = false, message = "Error al eliminar producto" });
    }

    public async Task<IActionResult> OnGetProductoAsync(int id)
    {
        var producto = await _productoService.GetProductoConPreciosAsync(id);
        return new JsonResult(producto);
    }
}