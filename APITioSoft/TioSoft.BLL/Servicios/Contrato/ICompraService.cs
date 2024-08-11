﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TioSoft.DTO;

namespace TioSoft.BLL.Servicios.Contrato
{
    public interface ICompraService
    {
        Task<CompraDTO> Registrar(CompraDTO modelo);
        Task<List<CompraDTO>> Historial(string buscarPor, string numeroCompra, string fechaInicio, string fechaFin);
    }
}
