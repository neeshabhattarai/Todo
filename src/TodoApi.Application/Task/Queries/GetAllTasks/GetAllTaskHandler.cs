using AutoMapper;
using MediatR;
using TodoApi.Applicaiton.DTO;
using TodoApi.Domain.Repository;

using TodoApi.Application.Task.PageReturn;

namespace TodoApi.Application.Task.Queries.GetAllTasks;

public class GetAllTaskHandler(ITask taskRepo,IMapper mapper):IRequestHandler<GetAllTaskCommand,PageResult>
{
    public async Task<PageResult> Handle(GetAllTaskCommand request, CancellationToken cancellationToken)
    {
        var (result,totalCount) = await taskRepo.GetAllTask(request.searchParams, request.pageNumber, request.pageSize, request.orderBy, request.sortDirection);
        var mapperResult= mapper.Map<List<TaskDTO>>(result);
        return new PageResult(mapperResult,totalCount,request.pageNumber, request.pageSize, request.orderBy, request.sortDirection);
    }
}