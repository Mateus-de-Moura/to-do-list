using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using tasksWebApi.Application.Common.Extensions;
using tasksWebApi.Application.Common.Models;
using tasksWebApi.Application.Dtos;
using tasksWebApi.Domain.Entities;
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

        public async Task<Result<PaginatedList<tasksWebApi.Domain.Entities.Task>>> GetPagedAppUserAsync(GetPagedTaskRequestDto request)
        {
            try
            {
                var paged = await context.Tasks
                    .Where(t => string.IsNullOrEmpty(request.search) || t.Description.Contains(request.search))
                    .PaginatedListAsync(request.PageNumber, request.PageSize);


                return Result.Success(paged);
            }
            catch (Exception e)
            {
                return Result.Error(e.Message); ;
            }
        }

        public async Task<Result<bool>> CreateAsync(CreateTaskDto task)
        {

            await context.Tasks.AddAsync(new Domain.Entities.Task
            {
                Completed = task.Completed,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
            });

            int rowsAffected = await context.SaveChangesAsync();

            return rowsAffected > 0 ? Result.Success(true) : Result.Error();

        }
    }
}
