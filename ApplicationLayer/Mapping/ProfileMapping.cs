using ApplicationLayer.Dtos;
using AutoMapper;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Mapping
{
    public class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            CreateMap<RegisteredUsers,RegisteredUserDto>().ReverseMap();
            CreateMap<Tasks, TasksDto>().ReverseMap();
            CreateMap<AssignTasks, AssignTasksDto>().ReverseMap();
            CreateMap<UserSuccess,UserSuccessDto>().ReverseMap();
        }
    }
}
