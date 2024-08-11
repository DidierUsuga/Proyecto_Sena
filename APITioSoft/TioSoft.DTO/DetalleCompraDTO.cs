﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TioSoft.DTO
{
    public class DetalleCompraDTO
    {
        public int? IdProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public string? DescripcionProveedor { get; set; }

        public int? Cantidad { get; set; }

        public string? PrecioTexto { get; set; }

        public string? TotalTexto { get; set; }
    }
}
