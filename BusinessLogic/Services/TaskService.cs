using BusinessLogic.Contracts;
using DataAccessLayer.TaskDbContext;
using DTOs.Entities;
using DTOs.Responses;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TaskManagement.DataAccessLayer.Repositories;
using Task = DataAccessLayer.TaskDbContext.Task;

namespace BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<bool> CreateTaskAsync(ToDoTaskDto task)
        {
            _logger.LogInformation("Creating a new task");
            return await _taskRepository.AddAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            _logger.LogInformation("Deleting task with id {TaskId}", id);
            var result = await _taskRepository.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Task with id {TaskId} not found", id);
            }
            return result;
        }

        public async Task<ToDoTaskDto?> GetTaskAsync(int id)
        {
            _logger.LogInformation("Fetching task with id {TaskId}", id);
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Task with id {TaskId} not found", id);
            }
            return task;
        }

        public async Task<IEnumerable<ToDoTaskDto>> GetTasksAsync()
        {
            _logger.LogInformation("Fetching all tasks");
            return await _taskRepository.GetAllAsync();
        }

        public async Task<bool> UpdateTaskAsync(ToDoTaskDto task)
        {
            _logger.LogInformation("Updating task with id {TaskId}", task.Id);
            return await _taskRepository.UpdateAsync(task);
        }
    }
}