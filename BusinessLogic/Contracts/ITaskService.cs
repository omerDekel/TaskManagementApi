using DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTaskDto = DTOs.Entities.ToDoTaskDto;

namespace BusinessLogic.Contracts
{
    public interface ITaskService
    {
        Task<IEnumerable<ToDoTaskDto>> GetTasksAsync();
        Task<ToDoTaskDto?> GetTaskAsync(int id);
        Task<bool> CreateTaskAsync(ToDoTaskDto task);
        Task<bool> UpdateTaskAsync(ToDoTaskDto task);
        Task<bool> DeleteTaskAsync(int id);
    }
}
