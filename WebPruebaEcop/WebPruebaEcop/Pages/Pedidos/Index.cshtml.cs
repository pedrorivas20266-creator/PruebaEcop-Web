using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;

namespace WebPruebaEcop.Pages.Pedidos;

public class IndexModel : PageModel
{
    private readonly IPedidoService _pedidoService;
    private readonly IClienteService _clienteService;
    private readonly IProductoService _productoService;

    public PaginatedResponse<PedidoResponse> Pedidos { get; set; } = new();
    public List<Cliente> Clientes { get; set; } = new();
    public List<ProductoQuery> Productos { get; set; } = new();

    [BindProperty]
    public PedidoRequest Pedido { get; set; } = new();

    [BindProperty]
    public string? Busqueda { get; set; }

    [BindProperty]
    public DateTime? FechaDesde { get; set; }

    [BindProperty]
    public DateTime? FechaHasta { get; set; }

    public IndexModel(
        IPedidoService pedidoService,
        IClienteService clienteService,
        IProductoService productoService)
    {
        _pedidoService = pedidoService;
        _clienteService = clienteService;
        _productoService = productoService;
    }

    public async Task<IActionResult> OnGetAsync(
        string? busqueda = null,
        DateTime? fechaDesde = null,
        DateTime? fechaHasta = null,
        int pageNumber = 1)
    {
        var token = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToPage("/Login");
        }

        Busqueda = busqueda;
        FechaDesde = fechaDesde;
        FechaHasta = fechaHasta;

        Pedidos = await _pedidoService.GetPedidosAsync(busqueda, fechaDesde, fechaHasta, pageNumber, 10);
        Clientes = await _clienteService.GetClientesActivosAsync();
        Productos = await _productoService.GetProductosAsync(null, 1000);

        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync([FromBody] PedidoRequest pedido)
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(new { success = false, message = "Datos inválidos" });
        }

        var codUsuario = HttpContext.Session.GetInt32("CodUsuario");
        if (codUsuario == null)
        {
            return new JsonResult(new { success = false, message = "Sesión expirada" });
        }

        pedido.NumPedido = $"PED-{DateTime.Now:yyyyMMdd-HHmmss}";
        pedido.CodUsuario = codUsuario.Value;
        pedido.Fecha = DateTime.Now;

        var result = await _pedidoService.CreatePedidoAsync(pedido);

        if (result != null)
        {
            return new JsonResult(new { success = true, message = "Pedido creado exitosamente", data = result });
        }

        return new JsonResult(new { success = false, message = "Error al crear pedido" });
    }

    public async Task<IActionResult> OnPostAnularAsync([FromBody] AnularRequest request, int id)
    {
        var result = await _pedidoService.AnularPedidoAsync(id, request.Motivo);

        if (result != null)
        {
            return new JsonResult(new { success = true, message = "Pedido anulado exitosamente" });
        }

        return new JsonResult(new { success = false, message = "Error al anular pedido" });
    }

    public async Task<IActionResult> OnGetPedidoAsync(int id)
    {
        var pedido = await _pedidoService.GetPedidoByIdAsync(id);
        return new JsonResult(pedido);
    }

    public async Task<IActionResult> OnGetClientesAsync()
    {
        var clientes = await _clienteService.GetClientesActivosAsync();
        return new JsonResult(clientes);
    }

    public async Task<IActionResult> OnGetProductosAsync()
    {
        var productos = await _productoService.GetProductosAsync(null, 1000);
        return new JsonResult(productos);
    }
}

public class AnularRequest
{
    public string Motivo { get; set; } = string.Empty;
}