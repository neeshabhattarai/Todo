using AutoMapper;
using NUnit.Framework;
using Moq;
using TodoApi.Applicaiton.Task.Command.CreateTask;
using TodoApi.Application.User;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.UnitTest.Task.Command.CreateTask
{
    public class CreateTaskHandlerTest
    {
        private CreateTaskHandler _handler;
        private Mock<ITask> _task;
        private Mock<IMapper> _mapper;
        private Mock<IUserContext> _userContext;

        [SetUp]
        public void Setup()
        {
            _task = new Mock<ITask>();
            _mapper = new Mock<IMapper>();
            _userContext = new Mock<IUserContext>();
            _handler = new(_task.Object, _mapper.Object, _userContext.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task CreateTaskHandler_CreateTask_ShouldCreateTask()
        {

            var task = new Applicaiton.Task.Command.CreateTask.CreateTask
            {
                Name = "test",
                Description = "test",
                IsCompleted = false
            };
            var tasks = new Tasks
            {
                Name = "test",
                Description = "test",
                IsCompleted = false,
                Id = 33,
                CreatedAt = DateTime.Now,
                UserId = "222"
            };
            _mapper.Setup(x => x.Map<Tasks>(task)).Returns(tasks);
            _userContext.Setup(x => x.GetCurrentUser())
                .Returns(new CurrentUser("222", "test@gmail.com", "test", "Admin"));

            _task.Setup(x => x.CreateTask(It.IsAny<Tasks>())).ReturnsAsync(33);
            var result = await _handler.Handle(task, CancellationToken.None);
            _mapper.Verify(x => x.Map<Tasks>(task), Times.Once);
            Assert.That(result, Is.TypeOf<int>());

        }

        [Test]
        public async System.Threading.Tasks.Task CreateTaskHandler_WithoutMapper_ShouldCreateTask()
        {
            var task = new Applicaiton.Task.Command.CreateTask.CreateTask
            {
                Name = "test",
                Description = "test",
                IsCompleted = false
            };
            _mapper.Setup(x => x.Map<Tasks>(task)).Returns((Tasks)null);
            _userContext.Setup(x => x.GetCurrentUser())
                .Returns(new CurrentUser("222", "test@gmail.com", "test", "Admin"));

            _task.Setup(x => x.CreateTask(It.IsAny<Tasks>())).ReturnsAsync(33);
            var result = Assert.ThrowsAsync<NullReferenceException>(async ()=>await _handler.Handle(task, CancellationToken.None));
            _mapper.Verify(x => x.Map<Tasks>(task), Times.Once);
            _userContext.Verify(x => x.GetCurrentUser(), Times.Once);
            Assert.That(result, Is.TypeOf<NullReferenceException>());
            Assert.That(result.Message, Does.Contain("Object reference not set to an instance of an object."));
        }
        [Test]
        public async System.Threading.Tasks.Task CreateTaskHandler_WithoutUser_ShouldCreateTask()
        {

            var task = new Applicaiton.Task.Command.CreateTask.CreateTask
            {
                Name = "test",
                Description = "test",
                IsCompleted = false
            };
            _task.Setup(x => x.CreateTask(It.IsAny<Tasks>())).ReturnsAsync(33);
            _userContext.Setup(x => x.GetCurrentUser()).Returns((CurrentUser)null);
            var result = Assert.ThrowsAsync<InvalidOperationException>(async ()=>await _handler.Handle(task, CancellationToken.None));
            _mapper.Verify(x => x.Map<Tasks>(task), Times.Once);
            _userContext.Verify(x => x.GetCurrentUser(), Times.Once);
            Assert.That(result, Is.TypeOf<InvalidOperationException>());
            Assert.That(result.Message, Does.Contain("UserContext is null"));

        }
    }


}

