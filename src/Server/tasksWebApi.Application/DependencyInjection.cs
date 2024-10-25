using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasksWebApi.Application.Services;

namespace tasksWebApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<TasksService>();

            return services;
        }

    }
}
