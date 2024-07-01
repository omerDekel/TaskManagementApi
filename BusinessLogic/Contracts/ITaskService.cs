using DirectModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDto = DirectModels.Entities.TaskDto;

namespace BusinessLogic.Contracts
{
    public interface ITaskService
    {
        Response GetTasks();
        Response GetTask(int id);
        Response CreateTask(TaskDto task);
        Response UpdateTask(TaskDto task);
        Response DeleteTask(int id);
    }
}
