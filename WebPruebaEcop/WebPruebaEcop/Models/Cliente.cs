namespace RazorPagesApp.Models;

public class Cliente
{
    public int CodCliente { get; set; }
    public string NumCliente { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public int CodTipoDocumento { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public string NumeroTelefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public DateTime? FecGra { get; set; }
}