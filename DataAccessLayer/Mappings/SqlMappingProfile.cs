using AutoMapper;
using DTOs.Entities;
using System;
using DataAccessLayer.TaskDbContext;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlToDoTask = DataAccessLayer.TaskDbContext.SqlToDoTask;

namespace TaskManagement.DataAccessLayer.Mappings
{
    public class SqlMappingProfile : Profile
    {
        public SqlMappingProfile()
        {
            CreateMap<SqlToDoTask, ToDoTaskDto>().ReverseMap();
        }
    }
}
