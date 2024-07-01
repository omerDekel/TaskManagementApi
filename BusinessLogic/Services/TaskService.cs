using BusinessLogic.Contracts;
using DataAccessLayer.TaskDbContext;
using DirectModels.Entities;
using DirectModels.Responses;
using Task = DataAccessLayer.TaskDbContext.Task;

namespace BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        TaskManagementContext _context;

        public TaskService(TaskManagementContext context)
        {
            _context = context;
        }

        public Response CreateTask(DirectModels.Entities.TaskDto task)
        {
            throw new NotImplementedException();
        }

        public Response DeleteTask(int id)
        {
            throw new NotImplementedException();
        }

        public Response GetTask(int id)
        {
            throw new NotImplementedException();
        }

        public Response GetTasks()
        {
            Response response = new Response();
            List<Task> allTasks = null;
            try
            {
                allTasks = _context.Tasks.ToList();
                response.IsSuccess = allTasks.Any();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public Response UpdateTask(DirectModels.Entities.TaskDto task)
        {
            throw new NotImplementedException();
        }
    }
}