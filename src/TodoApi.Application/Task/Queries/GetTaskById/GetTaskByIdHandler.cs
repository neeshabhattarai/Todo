using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using MediatR;
using TodoApi.Applicaiton.DTO;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Exception;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.Task.Queries.GetTaskById;

public class GetTaskByIdHandler(IMapper mapper,ITask taskRepo):IRequestHandler<GetTaskByIdCommand,TaskDTO>
{
    public async Task<TaskDTO> Handle(GetTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepo.GetTaskById(request.TaskId);
        if (task == null)
        {
            throw new NotFoundException(nameof(Tasks), request.TaskId.ToString());
        }
        return mapper.Map<TaskDTO>(task);
    }
}