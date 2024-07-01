using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }
        [HttpGet]
        public  IActionResult GetTasks()
        {
            return Ok(taskService.GetTasks());
        }
    }
}