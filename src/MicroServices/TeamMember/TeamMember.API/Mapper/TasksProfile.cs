using AutoMapper;
using EventBus.Messages.Events;
using TeamMember.API.Entities;
using TeamMember.API.Feature;

namespace TeamMember.API.Mapper
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<TaskCreation, TaskEvents>().ReverseMap();
            CreateMap<Tasks, TaskCreation>().ReverseMap();

        }
    }
}
