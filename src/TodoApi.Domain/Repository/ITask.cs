using TodoApi.Domain.Entities;

namespace TodoApi.Domain.Repository;

public interface ITask
{
    Task<int> CreateTask(Tasks task);
    Task<(List<Tasks>,int)> GetAllTask(string? searchParams,int pageNumber,int pageSize,string? orderBy,string? sortDirection);
    Task<Tasks> GetTaskById(int taskId);
    Task DeleteTask(Tasks task);
}