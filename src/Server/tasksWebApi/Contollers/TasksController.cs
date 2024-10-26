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

            const int PAGE_SIZE = 10;

            var result = await tasksService.GetPagedAppUserAsync(new GetPagedTaskRequestDto(model.PageNumber, PAGE_SIZE, query));

            var response = new DataTablesJsonResult<Domain.Entities.Task>(
                model.Draw,
                result.Value.TotalCount,
                result.Value.TotalPages,
                result.Value.Items);

            return Ok(response);
        }


    }
}
