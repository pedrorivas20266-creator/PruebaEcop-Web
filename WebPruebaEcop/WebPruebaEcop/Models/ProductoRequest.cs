namespace RazorPagesApp.Models;

public class ProductoRequest
{
    public string NumProducto { get; set; } = string.Empty;
    public string CodigoBarra { get; set; } = string.Empty;
    public string DesProducto { get; set; } = string.Empty;
    public int CodCategoria { get; set; }
    public int CodUnidadMedida { get; set; }
    public int CodIva { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public double? CostoPromedio { get; set; }
    public double? CostoUltimo { get; set; }
    public bool DescuentaStock { get; set; }
    public List<PrecioRequest> Precios { get; set; } = new();
}

public class PrecioRequest
{
    public string NumPrecio { get; set; } = string.Empty;
    public string DesPrecio { get; set; } = string.Empty;
    public int CodTipoPrecio { get; set; }
    public double? PrecioVenta { get; set; }
}