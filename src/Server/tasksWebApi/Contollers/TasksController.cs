using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tasksWebApi.Application.Services;

namespace tasksWebApi.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(TasksService tasksService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await tasksService.getAll();

            return result.IsSuccess ? Ok(result.Value) : BadRequest();    
        }
    }
}
