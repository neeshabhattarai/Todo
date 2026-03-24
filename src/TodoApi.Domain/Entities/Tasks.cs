using System.ComponentModel.DataAnnotations;

namespace TodoApi.Domain.Entities;

public class Tasks
{
    [Key]
    public int Id { get; set; }
    public string Task{ get; set; }
    public string Description { get; set; }
}