namespace TodoApi.Applicaiton.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UserName { get; set; }
    
}