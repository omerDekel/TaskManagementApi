using BusinessLogic.Contracts;
using DTOs.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            this._taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTaskDto>>> GetTasks()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTaskDto>> GetTask(int id)
        {
            var task = await _taskService.GetTaskAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(ToDoTaskDto taskDto)
        {
            await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, ToDoTaskDto taskDto)
        {
            if (id != taskDto.Id)
            {
                return BadRequest();
            }
            var res = await _taskService.UpdateTaskAsync(taskDto);
            if (res)
            {
                return Ok(taskDto);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the task.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
            {
                return NotFound(); // Task not found or deletion failed
            }
            return NoContent();
        }
    }
}