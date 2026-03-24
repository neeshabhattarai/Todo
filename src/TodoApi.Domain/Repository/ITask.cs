using TodoApi.Domain.Entities;

namespace TodoApi.Domain.Repository;

public interface ITask
{
    Task<int> CreateTask(Tasks task);
    List<Tasks> GetAllTask();
    Task<Tasks> GetTaskById(int taskId);
}