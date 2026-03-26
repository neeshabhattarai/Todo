using Microsoft.AspNetCore.Identity;

namespace TodoApi.Domain.Entities;

public class User:IdentityUser
{
 public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
}