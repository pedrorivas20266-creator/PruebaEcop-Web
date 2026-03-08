using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Interfaces;

namespace WebPruebaEcop.Pages;

public class LoginModel : PageModel
{
    private readonly IAuthService _authService;

    [BindProperty]
    public LoginRequest LoginRequest { get; set; } = new();

    public string ErrorMessage { get; set; } = string.Empty;

    public LoginModel(IAuthService authService)
    {
        _authService = authService;
    }

    public void OnGet()
    {
        // Limpiar sesión si existe
        HttpContext.Session.Clear();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Por favor complete todos los campos";
            return Page();
        }

        var response = await _authService.LoginAsync(LoginRequest);

        if (response == null || string.IsNullOrEmpty(response.AccessToken))
        {
            ErrorMessage = "Usuario o contraseña incorrectos";
            return Page();
        }

        // Guardar token y datos de usuario en sesión
        HttpContext.Session.SetString("AccessToken", response.AccessToken);
        HttpContext.Session.SetString("Usuario", response.Usuario.Nombres);
        HttpContext.Session.SetInt32("CodUsuario", response.Usuario.CodUsuario);

        return RedirectToPage("/Index");
    }
}