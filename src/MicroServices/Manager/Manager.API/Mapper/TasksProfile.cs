using AutoMapper;
using EventBus.Messages.Events;
using Manager.API.Entities;

namespace Manager.API.Mapper
{
    public class TasksProfile: Profile
    {
        public TasksProfile()
        {
            CreateMap<Tasks, TaskEvents>().ReverseMap();
        }
    }
}
