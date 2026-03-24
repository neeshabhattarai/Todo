using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;
using TodoApi.Infastructure.Persistency;

namespace TodoApi.Infastructure.Repository;

public class TaskRepository(TodoApplicationDbContext context):ITask
{
    public async Task<int> CreateTask(Tasks task)
    {
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();
        return task.Id;
    }

    public List<Tasks> GetAllTask()
    {
        var list = context.Tasks.ToList();
        return list;
    }

    public async Task<Tasks> GetTaskById(int taskId)
    {
       var task= await context.Tasks.FindAsync(taskId);
       return task;
        
    }
}