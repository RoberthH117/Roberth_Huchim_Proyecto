using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin_ioc
{
    public static class Dependencia
    {

        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QualfixAdminContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("cadenaSQL"));
                 
            });
        }

        public static void InyectarDependenciasSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QualfixAdminSecurityContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SeguridadQualfixAdmin"));
                      
            });
        }







    }
}
