using System;
using System.Collections.Generic;

namespace TioSoft.Model;

public partial class Compra
{
    public int IdCompra { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompra { get; set; } = new List<DetalleCompra>();
}
