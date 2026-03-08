namespace RazorPagesApp.Models;

public class Producto
{
    public int CodProducto { get; set; }
    public string NumProducto { get; set; } = string.Empty;
    public string CodigoBarra { get; set; } = string.Empty;
    public string DesProducto { get; set; } = string.Empty;
    public int CodCategoria { get; set; }
    public int CodUnidadMedida { get; set; }
    public int CodIva { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public double? CostoPromedio { get; set; }
    public double? CostoUltimo { get; set; }
    public bool Activo { get; set; }
    public bool DescuentaStock { get; set; }
    public DateTime? FecGra { get; set; }
}

public class ProductoConPrecioResponse : Producto
{
    public List<PrecioResponse> Precios { get; set; } = new();
}

public class PrecioResponse
{
    public int CodPrecio { get; set; }
    public string NumPrecio { get; set; } = string.Empty;
    public string DesPrecio { get; set; } = string.Empty;
    public int CodTipoPrecio { get; set; }
    public string DesTipoPrecio { get; set; } = string.Empty;
    public double? PrecioVenta { get; set; }
    public bool Activo { get; set; }
}