using AutoMapper;
using DataAccessLayer.TaskDbContext;
using DTOs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccessLayer.Contracts;
using ToDoTask = DataAccessLayer.TaskDbContext.ToDoTask;

namespace TaskManagement.DataAccessLayer.Repositories
{
    public class SqlTaskRepository : ITaskRepository
    {
        private readonly TaskManagementContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SqlTaskRepository> _logger;


        public SqlTaskRepository(TaskManagementContext context, IMapper mapper, ILogger<SqlTaskRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task<bool> AddAsync(ToDoTaskDto taskDto)
        {
            _logger.LogInformation("Adding a new task to the database");
            var task = _mapper.Map<ToDoTask>(taskDto);
            try
            {
                await _context.Tasks.AddAsync(task);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task with Title {Title}", taskDto.Title);
                return false;
            }
        }

        public async System.Threading.Tasks.Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting task with id {TaskId} from the database", id);
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    _logger.LogWarning("Task with id {TaskId} not found", id);
                    return false;
                }

                _context.Tasks.Remove(task);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task with id {TaskId}", id);
                return false;
            }
        }


        public async Task<IEnumerable<ToDoTaskDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all tasks from the database");

            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                return _mapper.Map<IEnumerable<ToDoTaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all tasks.");
                throw;
            }
        }

        public async Task<ToDoTaskDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching task with id {TaskId} from the database", id);
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                return _mapper.Map<ToDoTaskDto>(task);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching task with id {TaskId}", id);
                throw;
            }
        }


        public async System.Threading.Tasks.Task<bool> UpdateAsync(ToDoTaskDto taskDto)
        {
            _logger.LogInformation("Updating task with id {TaskId} in the database", taskDto.Id);
            var task = _mapper.Map<ToDoTask>(taskDto);
            try
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task with id {TaskId}", taskDto.Id);
                return false;
            }
        }
    }
}
