using TodoApi.Applicaiton.DTO;
using TodoApi.Domain.Constant;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Task.PageReturn;

public class PageResult
{
    public List<TaskDTO> tasks { get; set; }
    public int totalCount { get; set; }
    public int FromStartIndex { get; set; }
    public int FromEndIndex { get; set; }
    public string? orderBy { get; set; }
    public string? sortDirection { get; set; }

    public PageResult(List<TaskDTO> tasks, int totalCount, int pageNumber, int pageSize,string? orderBy,string? sortBy)
    {
        this.tasks = tasks;
        this.totalCount = totalCount;
        this.FromStartIndex = pageNumber * pageSize+1;
        this.FromEndIndex = FromEndIndex+pageSize-1;
        this.orderBy = orderBy;
        this.sortDirection = sortBy==SortDirection.Descending ? "Descending" : "Ascending";
    }
    
}