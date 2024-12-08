using AutoMapper;
using DTOs.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccessLayer.TaskDbContext;

namespace TaskManagement.DataAccessLayer.Mappings
{
    public class MongoMappingProfile : Profile
    {
        public MongoMappingProfile()
        {
            CreateMap<MongoToDoTask, ToDoTaskDto>()
           .ForMember(dest => dest.PersistentId, opt => opt.MapFrom(src => src._id)) // Map _id to PersistentId
           .ReverseMap()
           .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.PersistentId)) // Map PersistentId back to _id
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)); // Explicit for clarity, though optional

        }
    }
}