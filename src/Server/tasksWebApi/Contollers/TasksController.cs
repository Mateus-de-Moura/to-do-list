using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tasksWebApi.Application.Common.Libraries.DataTables;
using tasksWebApi.Application.Dtos;
using tasksWebApi.Application.Services;
using tasksWebApi.Domain.Entities;


namespace tasksWebApi.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(TasksService tasksService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await tasksService.GetAll();

            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> GetDataTables(DataTablesParameterModel model)
        {
            var query = Request.Form["query"].FirstOrDefault();
            var PAGE_NUMBER = Request.Form["pageNumber"].FirstOrDefault();

            const int PAGE_SIZE = 10;

            var result = await tasksService.GetPagedAppUserAsync(new GetPagedTaskRequestDto(int.Parse(PAGE_NUMBER), PAGE_SIZE, query));

            var response = new DataTablesJsonResult<Domain.Entities.Task>(
                model.Draw,
                result.Value.TotalCount,
                result.Value.TotalPages,
                result.Value.Items);

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateTaskDto task)
        {
            var result = await tasksService.CreateAsync(task);

            return result.IsSuccess ? Ok() : BadRequest();
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var result = await tasksService.GetAsync(Id);

            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }


    }
}
