using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<Result<List<Domain.Entities.Task>>> GetAll()
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

        public async Task<Result<PaginatedList<Domain.Entities.Task>>> GetPagedAppUserAsync(GetPagedTaskRequestDto request)
        {
            try
            {
                var paged = await context.Tasks
                    .Where(t => string.IsNullOrEmpty(request.search) || t.Description.Contains(request.search))
                    .OrderByDescending(x => x.CreatedAt)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);


                return Result.Success(paged);
            }
            catch (Exception e)
            {
                return Result.Error(e.Message);
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

        public async Task<Result<Domain.Entities.Task>> GetAsync(Guid Id)
        {
            try
            {
                var entity = await context.Tasks.FirstOrDefaultAsync(t => t.Id == Id);

                return entity is not null ? Result.Success(entity) : Result.Error();
            }
            catch (Exception e)
            {

                return Result.Error(e.Message);
            }
        }

        public async Task<Result<Domain.Entities.Task>> UpdateAsync(UpdateTaskDto model)
        {

            var entityExists = await context.Tasks.FirstOrDefaultAsync(t => t.Id.Equals(model.Id));

            if (entityExists is null)
                return Result.NotFound("Task não encontrada");

            entityExists.Completed = model.Completed;
            entityExists.Description = model.Description;

            var rowsAffected = await context.SaveChangesAsync();

            return rowsAffected > 0 ?
                Result.Success(entityExists) :
                Result.Error("Erro ao atualizar Task");
        }

        public async Task<Result<bool>> DeleteTaskAsync(Guid Id)
        {
            var entityExists = await context.Tasks.FirstOrDefaultAsync(t => t.Id.Equals(Id));

            if (entityExists is null)
                return Result.NotFound("Task não encontrada");


            context.Tasks.Remove(entityExists);

            var rowsAffected = await context.SaveChangesAsync();

            return rowsAffected > 0 ?
                Result.Success(true) :
                Result.Error("Erro ao atualizar Task");
        }
    }
}
