using AutoMapper;
using BusinessLogic.Services;
using DTOs.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccessLayer.Contracts;

namespace TaskManagementApi.Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;
        private readonly TaskService _service;
        private readonly Mock<ILogger<TaskService>> _logger;

        public TaskServiceTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            _logger = new Mock<ILogger<TaskService>>();
            _service = new TaskService(_repositoryMock.Object, _logger.Object);

        }

        [Fact]
        public async Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<ToDoTaskDto>
            {
                new ToDoTaskDto { Id = 1, Title = "Task 1" },
                new ToDoTaskDto { Id = 2, Title = "Task 2" }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tasks);

            // Act
            IEnumerable<ToDoTaskDto> result = await _service.GetTasksAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Task 1", result.FirstOrDefault().Title);
        }
    }
}
