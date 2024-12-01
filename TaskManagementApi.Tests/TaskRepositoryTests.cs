using AutoMapper;
using DataAccessLayer.TaskDbContext;
using DTOs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagement.DataAccessLayer.Repositories;

namespace TaskManagementApi.Tests
{
    public class TaskRepositoryTests
    {
        private readonly TaskManagementContext _context;
        private readonly SqlTaskRepository _repository;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<SqlTaskRepository>> _logger;



        public TaskRepositoryTests()
        {
            _mapperMock = new Mock<IMapper>();
            _logger = new Mock<ILogger<SqlTaskRepository>>();
            var options = new DbContextOptionsBuilder<TaskManagementContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTest")
                .Options;
            _context = new TaskManagementContext(options);
            _repository = new SqlTaskRepository(_context, _mapperMock.Object, _logger.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddTask()
        {
            var taskDto = new ToDoTaskDto { Id = 1, Title = "Test Task" };
            var task = new ToDoTask { Id = 1, Title = "Test Task" };
            _mapperMock.Setup(m => m.Map<ToDoTask>(taskDto)).Returns(task);

            await _repository.AddAsync(taskDto);
            await _context.SaveChangesAsync();

            var addedTask = await _context.Tasks.FindAsync(1);

            Assert.NotNull(addedTask);
            if (addedTask != null)
            {
                Assert.Equal("Test Task", addedTask.Title);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTasks()
        {
            _context.Tasks.Add(new ToDoTask { Id = 3, Title = "Task 1" });
            _context.Tasks.Add(new ToDoTask { Id = 4, Title = "Task 2" });
            await _context.SaveChangesAsync();

            var taskDtos = new List<ToDoTaskDto>
            {
                new() { Id = 3, Title = "Task 1" },
                new() { Id = 4, Title = "Task 2" }
            };

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ToDoTaskDto>>(It.IsAny<IEnumerable<ToDoTask>>())).Returns(taskDtos);

            var tasks = await _repository.GetAllAsync();

            Assert.Equal(2, tasks.Count());
            Assert.Equal("Task 1", tasks.First().Title);
        }

    }
}
