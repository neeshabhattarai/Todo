using System.Text.Json.Serialization;
using MediatR;

namespace TodoApi.Applicaiton.Task.Command.CreateTask;
public class CreateTask:IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CreatedAt { get; set; }=new DateTime();
}