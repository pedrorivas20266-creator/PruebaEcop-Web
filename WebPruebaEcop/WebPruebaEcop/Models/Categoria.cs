namespace RazorPagesApp.Models;

public class Categoria
{
    public int CodCategoria { get; set; }
    public string DesCategoria { get; set; } = string.Empty;
    public bool Activo { get; set; }
}