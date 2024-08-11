using System;
using System.Collections.Generic;

namespace TioSoft.Model;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public int? IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
