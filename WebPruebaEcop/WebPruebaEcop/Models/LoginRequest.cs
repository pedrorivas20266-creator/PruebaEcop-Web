namespace RazorPagesApp.Models;

public class LoginRequest
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string ClaveUsuario { get; set; } = string.Empty;
}