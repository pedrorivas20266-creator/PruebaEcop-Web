using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;

namespace WebPruebaEcop.Pages.Clientes;

public class IndexModel : PageModel
{
    private readonly IClienteService _clienteService;

    public List<Cliente> Clientes { get; set; } = new();

    [BindProperty]
    public Cliente Cliente { get; set; } = new();

    [BindProperty]
    public string? Busqueda { get; set; }

    public IndexModel(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    public async Task<IActionResult> OnGetAsync(string? busqueda = null)
    {
        var token = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToPage("/Login");
        }

        Busqueda = busqueda;
        Clientes = await _clienteService.GetClientesAsync(busqueda, 100);
        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync([FromBody] Cliente cliente)
    {
        if (cliente == null)
        {
            return new JsonResult(new { success = false, message = "Datos inválidos" });
        }

        var result = await _clienteService.CreateClienteAsync(cliente);

        if (result != null)
        {
            return new JsonResult(new { success = true, message = "Cliente creado exitosamente", data = result });
        }

        return new JsonResult(new { success = false, message = "Error al crear cliente" });
    }
    public async Task<IActionResult> OnPostUpdateAsync([FromBody] Cliente cliente, int id)
    {
        if (cliente == null)
        {
            return new JsonResult(new { success = false, message = "Datos inválidos" });
        }

        var result = await _clienteService.UpdateClienteAsync(id, cliente);

        if (result != null)
        {
            return new JsonResult(new { success = true, message = "Cliente actualizado exitosamente", data = result });
        }

        return new JsonResult(new { success = false, message = "Error al actualizar cliente" });
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var result = await _clienteService.DeleteClienteAsync(id);

        if (result)
        {
            return new JsonResult(new { success = true, message = "Cliente eliminado exitosamente" });
        }

        return new JsonResult(new { success = false, message = "Error al eliminar cliente" });
    }

    public async Task<IActionResult> OnGetClienteAsync(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        return new JsonResult(cliente);
    }
}