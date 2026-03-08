using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPruebaEcop.Pages;

public class IndexModel : PageModel
{
    public string Usuario { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        var token = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToPage("/Login");
        }

        Usuario = HttpContext.Session.GetString("Usuario") ?? "Usuario";
        return Page();
    }

    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Login");
    }
}