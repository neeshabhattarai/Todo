using AutoMapper;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using TodoApi.Domain.Entities;

namespace TodoApi.Applicaiton.DTO;

public class TaskMapper:Profile
{
    public TaskMapper()
    {
        CreateMap<CreateTask,TaskDTO>().ReverseMap();
        CreateMap<Tasks,TaskDTO>().ReverseMap();
        
    }
}