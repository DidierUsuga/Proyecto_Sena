using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TioSoft.DAL.DBContext;
using TioSoft.DAL.Repositorios.Contrato;
using TioSoft.Model;

using TioSoft.DAL.Repositorios;

using TioSoft.Utility;
using TioSoft.BLL.Servicios.Contrato;
using TioSoft.BLL.Servicios;

namespace TioSoft.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TioSoftAngularContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));


            services.AddScoped<IRolService, RolService>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();

        }

    }
}