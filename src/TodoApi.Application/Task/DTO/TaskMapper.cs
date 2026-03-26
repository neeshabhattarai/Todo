using AutoMapper;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using TodoApi.Domain.Entities;

namespace TodoApi.Applicaiton.DTO;

public class TaskMapper:Profile
{
    public TaskMapper()
    {
        CreateMap<CreateTask,Tasks>().ReverseMap();
        CreateMap<Tasks,TaskDTO>().ForMember(src=>src.UserName,x=>x.MapFrom(s=>s.User.Email)).ReverseMap();
        
    }
}