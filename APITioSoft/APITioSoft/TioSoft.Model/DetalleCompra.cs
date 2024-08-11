using System;
using System.Collections.Generic;

namespace TioSoft.Model;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int? IdCompra { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
