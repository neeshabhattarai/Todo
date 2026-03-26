using System.ComponentModel.DataAnnotations;

namespace TodoApi.Domain.Entities;

public class Tasks
{
    [Key]
    public int Id { get; set; }
    public string Name{ get; set; }
    public string Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}