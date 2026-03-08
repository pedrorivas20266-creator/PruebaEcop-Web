namespace RazorPagesApp.Models;

public class LoginResponse
{
    public UsuarioResponse Usuario { get; set; } = new();
    public string AccessToken { get; set; } = string.Empty;
    public int TokenType { get; set; }
    public int ExpiresIn { get; set; }
}

public class UsuarioResponse
{
    public int CodUsuario { get; set; }
    public string NumUsuario { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
}