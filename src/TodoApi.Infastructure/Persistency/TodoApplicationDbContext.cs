using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Entities;

namespace TodoApi.Infastructure.Persistency;

public class TodoApplicationDbContext:IdentityDbContext<User>
{
   public TodoApplicationDbContext(DbContextOptions<TodoApplicationDbContext> options):base(options)
   {
      
   } 
   public DbSet<Tasks> Tasks { get; set; }
}