using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.TaskDbContext;
using DTOs.Entities;

namespace TaskManagement.DataAccessLayer.Repositories
{
    /// <summary>
    /// Interafce for additional methods specific to Task entity
    /// </summary>
    public interface ITaskRepository: IRepository<ToDoTaskDto>
    {
    }
}
