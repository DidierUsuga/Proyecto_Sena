using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TioSoft.DTO
{
    public class CompraDTO
    {
        public int IdCompra { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? TipoPago { get; set; }
        public string? TotalTexto { get; set; }
        public string? FechaRegistro { get; set; }
        public virtual ICollection<DetalleCompraDTO> DetalleCompra { get; set; }
    }
}
