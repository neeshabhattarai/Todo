using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit;
using TodoApi.Domain.Entities;
using TodoApi.Infastructure.Persistency;
using TodoApi.Infastructure.Repository;

namespace TodoApi.Infastructure.UnitTest.Repository;
[TestFixture]
public class TaskRepositoryTest
{
   private TaskRepository _repository;

   private Tasks task1;
   private Tasks task2;
   private User _user;
   [SetUp]
   public void Setup()
   {
      task1 = new Tasks
      {
         Id = 1,
         Name = "Task1",
         Description = "Task1",
         UserId = "22"
      };
      task2 = new Tasks
      {
         Id = 2,
         Name = "Task2",
         Description = "Task2",
         UserId = "22"
      };
      _user = new User
      {
         Id = "22",
         UserName = "test",
         Email = "test@gmail.com"
      };


   }

   private DbContextOptionsBuilder<TodoApplicationDbContext> GetBuilder()
   {
      var builder = new DbContextOptionsBuilder<TodoApplicationDbContext>().UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString());
return  builder;
   }
   [Test]
   public async Task CreateTask_AddTask_ShouldReturnTrue()
   { 
      var builder =GetBuilder();
       using (var context = new TodoApplicationDbContext(builder.Options))
       {
          _repository = new TaskRepository(context);
          var result=await _repository.CreateTask(task1);
          Assert.IsNotNull(result);
          Assert.AreEqual(1, context.Tasks.Count());
          Assert.AreEqual(task1.Id, context.Tasks.First().Id);
          Assert.AreEqual(task1.Name, context.Tasks.First().Name);
          Assert.AreEqual(task1.UserId, context.Tasks.First().UserId);
       }
   }
   [Test]
   public async Task CreateTask_AddTaskWithoutUserId_ShouldReturnThrowsException()
   { 
      var builder =GetBuilder();
      using (var context = new TodoApplicationDbContext(builder.Options))
      {
         _repository = new TaskRepository(context);
         task1.UserId = null;
         var result=Assert.ThrowsAsync<DbUpdateException>(async ()=>await _repository.CreateTask(task1));
         Assert.IsNotNull(result);
         Assert.That(result, Is.TypeOf(typeof(DbUpdateException)));
         Assert.That(result.Message,Does.Contain("{'UserId'}"));
      }
   }
   [Test]
   public async Task GetTaskById_ShouldReturnTasks()
   { 
      var builder =GetBuilder();
      using var context = new TodoApplicationDbContext(builder.Options);
         _repository = new TaskRepository(context);
        await context.Users.AddAsync(_user);
        await context.SaveChangesAsync();
        await _repository.CreateTask(task1);
         await _repository.CreateTask(task2);
         Assert.AreEqual(2, context.Tasks.Count());
        var result=await _repository.GetTaskById(task1.Id);
        Assert.IsNotNull(result);
        Assert.True(context.Tasks.Count()>=1);
        Assert.AreEqual(task1.Id, result.Id);
        Assert.AreEqual(task1.Name, result.Name);
      
      
   }

   [Test] 
   public async Task GetTaskById_ShouldReturnNull()
   { 
      var builder =GetBuilder();
     
      using (var context = new TodoApplicationDbContext(builder.Options))
      {
         _repository = new TaskRepository(context);
         await context.Users.AddAsync(_user);
         await context.SaveChangesAsync();
         var result=await _repository.GetTaskById(task1.Id);
         Assert.Null(result);
      }
      
   }
   [Test]
   public async Task DeleteTask_ShouldReturnTrue()
   {
      var builder = GetBuilder();
      using var context = new TodoApplicationDbContext(builder.Options) ;
        await context.Users.AddAsync(_user);
        await context.SaveChangesAsync();
        await context.Tasks.AddAsync(task1);
         _repository = new TaskRepository(context);
        await _repository.DeleteTask(task1);
        Assert.AreEqual(0, context.Tasks.Count());
         
   }
   [Test]
   public async Task DeleteTask_ShouldReturnException()
   {
      var builder = GetBuilder();
      using var context = new TodoApplicationDbContext(builder.Options);
      
         _repository = new TaskRepository(context);
        var result=Assert.ThrowsAsync<DbUpdateConcurrencyException>(async ()=> await _repository.DeleteTask(task1));
         Assert.That(result, Is.TypeOf(typeof(DbUpdateConcurrencyException)));
      
   }

   [Test]
   public async Task GetAllTasks_ShouldReturnTasks()
   {
      var builder = GetBuilder();
      using var context = new TodoApplicationDbContext(builder.Options);
      _repository = new TaskRepository(context);
      await context.Users.AddAsync(_user);
      await _repository.CreateTask(task1);
      await _repository.CreateTask(task2);
      var (tasks,count)= await _repository.GetAllTask(null, 1, 5, "Name", "asc");
      Assert.IsNotNull(tasks);
      Assert.AreEqual(tasks.Count(),count);
      
      
   }
   [Test]
   public async Task GetAllTasks_ShouldEmptyTasks()
   {
      var builder = GetBuilder();

      using (var context = new TodoApplicationDbContext(builder.Options))
      {
         _repository = new TaskRepository(context);
         var (tasks,count)= await _repository.GetAllTask(null, 1, 5, "Name", "asc");
         Assert.IsEmpty(tasks);
         Assert.AreEqual(0, count);
      }
      
   }
   [Test]
   public async Task GetAllTasks_WithInvalidOrderBy_ShouldThrowsException()
   {
      var builder = GetBuilder();

      using (var context = new TodoApplicationDbContext(builder.Options))
      {
         _repository = new TaskRepository(context);
         var result=Assert.ThrowsAsync<KeyNotFoundException>(async ()=>await _repository.GetAllTask(null, 1, 5, "Names", "asc"));
         Assert.That(result, Is.TypeOf(typeof(KeyNotFoundException)));
      }
      
   }

}