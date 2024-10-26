using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using tasksWebApi.Infra.Data.Contexts;

namespace tasksWebApi.Application.Services
{
    public class TasksService(AppDatabaseContext context)
    {
        public async Task<Result<List<tasksWebApi.Domain.Entities.Task>>> GetAll()
        {
            try
            {
                var tasks = await context.Tasks.ToListAsync();

                return Result.Success(tasks);
            }
            catch (Exception)
            {

                throw;
            }
         
        }
    }
}
