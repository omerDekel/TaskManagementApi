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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest._id, opt => opt.Ignore()); // Use MongoDB's automatic ObjectId
        }
    }
}