using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasksWebApi.Application.Services
{
    public class TasksService
    {

        public async Task<Result<List<tasksWebApi.Domain.Entities.Task>>> getAll()
        {
            List<tasksWebApi.Domain.Entities.Task> tasks = new();

            return Result.Success(tasks);
        }
    }
}
