using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TioSoft.DTO;

namespace TioSoft.BLL.Servicios.Contrato
{
    public interface IProveedorService
    {
        Task<List<ProveedorDTO>> Lista();
        Task<ProveedorDTO> Crear(ProveedorDTO modelo);
        Task<bool> Editar(ProveedorDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
