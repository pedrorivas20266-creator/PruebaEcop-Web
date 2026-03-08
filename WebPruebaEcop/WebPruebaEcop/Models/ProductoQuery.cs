namespace RazorPagesApp.Models;

public class ProductoQuery
{
    public int CodProducto { get; set; }
    public string NumProducto { get; set; } = string.Empty;
    public string CodigoBarra { get; set; } = string.Empty;
    public string DesProducto { get; set; } = string.Empty;
    public int CodCategoria { get; set; }
    public string DesCategoria { get; set; } = string.Empty;
    public int CodUnidadMedida { get; set; }
    public string DesUnidadMedida { get; set; } = string.Empty;
    public int CodIva { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public double? CostoPromedio { get; set; }
    public double? CostoUltimo { get; set; }
    public double PrecioVenta { get; set; }
    public bool Activo { get; set; }
    public bool DescuentaStock { get; set; }
    public DateTime? FecGra { get; set; }
}