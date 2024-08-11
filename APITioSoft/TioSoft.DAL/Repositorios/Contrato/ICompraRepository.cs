using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TioSoft.Model;

namespace TioSoft.DAL.Repositorios.Contrato
{
    public interface ICompraRepository : IGenericRepository<Compra>
    {
        Task<Compra> Registrar(Compra modelo);
    }
}
