using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tasksWebApi.Application.Dtos;
using tasksWebApi.Application.Services;
using tasksWebApi.Domain.Entities;
using tasksWebApi.Infra.Data.Contexts;
using Xunit;


namespace tasksWebApi.Tests.Services
{
    public class TasksServiceTests
    {
        private readonly AppDatabaseContext _context;
        private readonly TasksService _service;

        public TasksServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _context = new AppDatabaseContext(options);
            _service = new TasksService(_context);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_ReturnsSuccess_WithListOfTasks()
        {
            var tasks = new List<Domain.Entities.Task>
            {
                new Domain.Entities.Task { Id = Guid.NewGuid(), Description = "Task 1", Completed = false },
                new Domain.Entities.Task { Id = Guid.NewGuid(), Description = "Task 2", Completed = true }
            };

            await _context.Tasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();

            var result = await _service.GetAll();

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetPagedAppUserAsync_ReturnsSuccess_WithPaginatedList()
        {
            var tasks = new List<Domain.Entities.Task>
            {
                new Domain.Entities.Task { Id = Guid.NewGuid(), Description = "Task 1", Completed = false },
                new  Domain.Entities.Task { Id = Guid.NewGuid(), Description = "Task 2", Completed = true }
            };

            await _context.Tasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();

            var request = new GetPagedTaskRequestDto { PageNumber = 1, PageSize = 10 };

            var result = await _service.GetPagedAppUserAsync(request);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.TotalCount);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateAsync_ValidTask_ReturnsSuccess()
        {
            var newTask = new CreateTaskDto
            {
                Description = "New Task",
                Completed = false,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _service.CreateAsync(newTask);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAsync_ValidId_ReturnsSuccess_WithTask()
        {
            var taskId = Guid.NewGuid();
            var task = new Domain.Entities.Task { Id = taskId, Description = "Task 1", Completed = false };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            var result = await _service.GetAsync(taskId);

            Assert.True(result.IsSuccess);
            Assert.Equal(task, result.Value);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateAsync_ExistingTask_ReturnsSuccess()
        {
            var existingTask = new Domain.Entities.Task { Id = Guid.NewGuid(), Description = "Old Task", Completed = false };

            await _context.Tasks.AddAsync(existingTask);
            await _context.SaveChangesAsync();

            var updateDto = new UpdateTaskDto
            {
                Id = existingTask.Id,
                Description = "Updated Task",
                Completed = true
            };

            var result = await _service.UpdateAsync(updateDto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Updated Task", existingTask.Description);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteTaskAsync_ExistingTask_ReturnsError()
        {
            var taskId = Guid.NewGuid();            

            var result = await _service.DeleteTaskAsync(taskId);

            Assert.True(result.IsNotFound());
            Assert.Equal("Task não encontrada", result.Errors.First());
        }
    }
}
