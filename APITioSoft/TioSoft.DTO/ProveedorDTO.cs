using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TioSoft.DTO
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }

        public int? IdCategoria { get; set; }

        public string? DescripcionCategoria { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public int? EsActivo { get; set; }
    }
}
