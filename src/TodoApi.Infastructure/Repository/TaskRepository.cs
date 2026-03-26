using System.Collections.Immutable;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Constant;
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

    public async Task<(List<Tasks>,int)> GetAllTask(string? searchParams, int pageNumber, int
        pageSize,string? orderBy,string? sortDirection)
    {
        var list = context.Tasks.Include("User").Where(x=>searchParams==null || x.Description.Contains(searchParams) || x.Name.Contains(searchParams));
        
        list = list.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        var totalCount =await list.CountAsync();
        if (orderBy != null)
        {
            var allType=new Dictionary<string,Expression<Func<Tasks, object>>>
            {
                {nameof(Tasks.Id),x=>x.Id},
                {nameof(Tasks.Description),x=>x.Description},
                {nameof(Tasks.Name),x=>x.Name},
            };
            list=sortDirection==SortDirection.Ascending?list.OrderBy(allType[orderBy]):list.OrderByDescending(allType[orderBy]);
        }
        return (list.ToList(),totalCount);
    }

    public async Task<Tasks> GetTaskById(int taskId)
    {
       var task= await context.Tasks.Include("User").FirstAsync(x => x.Id == taskId);
       return task;
        
    }
    public async Task DeleteTask(Tasks task)
    {
        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
    }
}