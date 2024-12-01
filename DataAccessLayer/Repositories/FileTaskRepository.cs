using DataAccessLayer.TaskDbContext;
using DTOs.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagement.DataAccessLayer.Contracts;

namespace TaskManagement.DataAccessLayer.Repositories
{

    public class FileTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private readonly ILogger<FileTaskRepository> _logger;



        public FileTaskRepository(string filePath/*, ILogger<FileTaskRepository> logger*/)
        {
            _filePath = filePath;
            //_logger = logger;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public async Task<IEnumerable<ToDoTaskDto>> GetAllAsync()
        {
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<ToDoTaskDto>>(json) ?? new List<ToDoTaskDto>();
        }

        public async Task<ToDoTaskDto> GetByIdAsync(int id)
        {
            var tasks = await GetAllAsync();
            return tasks.FirstOrDefault(task => task.Id == id);
        }

        public async Task<bool> AddAsync(ToDoTaskDto task)
        {
            var tasks = (await GetAllAsync()).ToList();
            task.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(task);
            await SaveAllAsync(tasks);
            return true;
        }

        public async Task<bool> UpdateAsync(ToDoTaskDto task)
        {
            var tasks = (await GetAllAsync()).ToList();
            var index = tasks.FindIndex(t => t.Id == task.Id);
            if (index < 0) return false;
            tasks[index] = task;
            await SaveAllAsync(tasks);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tasks = (await GetAllAsync()).ToList();
            var removed = tasks.RemoveAll(t => t.Id == id);
            await SaveAllAsync(tasks);
            return removed > 0;
        }

        private async System.Threading.Tasks.Task SaveAllAsync(IEnumerable<ToDoTaskDto> tasks)
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}