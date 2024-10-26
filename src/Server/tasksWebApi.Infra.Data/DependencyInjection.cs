using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasksWebApi.Infra.Data.Contexts;

namespace tasksWebApi.Infra.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDatabaseContext>(options => options.UseSqlServer(configuration.GetValue<string>("CONNECTION_STRING")));

            return services;
        }
    }
}
