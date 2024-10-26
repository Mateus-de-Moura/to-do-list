using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasksWebApi.Infra.Data.Contexts;

namespace tasksWebApi.Infra.Data
{
    internal sealed class AppDataBaseContextFactory : IDesignTimeDbContextFactory<AppDatabaseContext>
    {
        public AppDatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDatabaseContext>();

            optionsBuilder.UseSqlServer();

            return new AppDatabaseContext(optionsBuilder.Options);

        }
    }
}
