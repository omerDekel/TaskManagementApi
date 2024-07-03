using AutoMapper;
using Castle.Core.Logging;
using DataAccessLayer.TaskDbContext;
using DTOs.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccessLayer.Repositories;
using Task = DataAccessLayer.TaskDbContext.Task;

namespace TaskManagementApi.Tests
{
    public class TaskRepositoryTests
    {
        private readonly TaskManagementContext _context;
        private readonly TaskRepository _repository;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<TaskRepository>> _logger;



        public TaskRepositoryTests()
        {
            _mapperMock = new Mock<IMapper>();
            _logger = new Mock<ILogger<TaskRepository>>();
            var options = new DbContextOptionsBuilder<TaskManagementContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementTest")
                .Options;
            _context = new TaskManagementContext(options);
            _repository = new TaskRepository(_context, _mapperMock.Object, _logger.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task AddAsync_ShouldAddTask()
        {
            var taskDto = new ToDoTaskDto { Id = 1, Title = "Test Task" };
            var task = new Task { Id = 1, Title = "Test Task" };
            _mapperMock.Setup(m => m.Map<Task>(taskDto)).Returns(task);

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
        public async System.Threading.Tasks.Task GetAllAsync_ShouldReturnAllTasks()
        {
            _context.Tasks.Add(new Task { Id = 3, Title = "Task 1" });
            _context.Tasks.Add(new Task { Id = 4, Title = "Task 2" });
            await _context.SaveChangesAsync();

            var taskDtos = new List<ToDoTaskDto>
            {
                new ToDoTaskDto { Id = 3, Title = "Task 1" },
                new ToDoTaskDto { Id = 4, Title = "Task 2" }
            };

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ToDoTaskDto>>(It.IsAny<IEnumerable<Task>>())).Returns(taskDtos);

            var tasks = await _repository.GetAllAsync();

            Assert.Equal(2, tasks.Count());
            Assert.Equal("Task 1", tasks.First().Title);
        }

    }
}
