namespace RazorPagesApp.Models;

public class PedidoRequest
{
    public string? NumPedido { get; set; }
    public DateTime? Fecha { get; set; }
    public int CodUsuario { get; set; }
    public int CodCliente { get; set; }
    public int CodMoneda { get; set; }
    public List<PedidoDetalleRequest> Detalles { get; set; } = new();
}

public class PedidoDetalleRequest
{
    public int CodProducto { get; set; }
    public double Cantidad { get; set; }
    public double PrecioUnitario { get; set; }
    public int LineaNumero { get; set; }
}

public class PedidoResponse
{
    public int CodPedido { get; set; }
    public string NumPedido { get; set; } = string.Empty;
    public DateTime? Fecha { get; set; }
    public int CodUsuario { get; set; }
    public int CodCliente { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string ApellidoCliente { get; set; } = string.Empty;
    public string DocumentoCliente { get; set; } = string.Empty;
    public int CodMoneda { get; set; }
    public double? Total { get; set; }
    public double? Iva { get; set; }
    public bool Activo { get; set; }
    public string? MotivoAnulacion { get; set; }
    public DateTime? FecGra { get; set; }
    public List<PedidoDetalleResponse> Detalles { get; set; } = new();
}

public class PedidoDetalleResponse
{
    public int CodPedidoDetalle { get; set; }
    public int CodProducto { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public double? Cantidad { get; set; }
    public double? PrecioUnitario { get; set; }
    public double? Subtotal { get; set; }
    public int LineaNumero { get; set; }
}

public class PaginatedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}