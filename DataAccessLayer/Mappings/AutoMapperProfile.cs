using AutoMapper;
using DTOs.Entities;
using System;
using DataAccessLayer.TaskDbContext;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DataAccessLayer.TaskDbContext.Task;

namespace TaskManagement.DataAccessLayer.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Task, ToDoTaskDto>().ReverseMap();
        }
    }
}
