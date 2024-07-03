using BusinessLogic.Contracts;
using BusinessLogic.Services;
using DataAccessLayer.TaskDbContext;
using DTOs.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity;
using TaskManagementApi.Controllers;

namespace TaskManagementApi.Tests;

public class TasksControllerTests
{
    private readonly Mock<ITaskService> _serviceMock;
    private readonly TasksController _controller;
    public TasksControllerTests()
    {
        _serviceMock = new Mock<ITaskService>();
        _controller = new TasksController(_serviceMock.Object);
    }
    [Fact]
    public async System.Threading.Tasks.Task GetAllTasks_ShouldReturnOkResult_WithListOfTasks()
    {
        // Arrange
        var taskDtos = new List<ToDoTaskDto>
            {
                new ToDoTaskDto { Id = 1, Title = "Task 1" },
                new ToDoTaskDto { Id = 2, Title = "Task 2" }
            };
        _serviceMock.Setup(service => service.GetTasksAsync()).ReturnsAsync(taskDtos);

        // Act
        var result = await _controller.GetTasks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnTasks = Assert.IsType<List<ToDoTaskDto>>(okResult.Value);
        Assert.Equal(2, returnTasks.Count);
    }
    [Fact]
    public async System.Threading.Tasks.Task GetTask_ShouldReturnsTask()
    {
        // Arrange
        var taskDto = new ToDoTaskDto { Id = 1, Title = "Task 1" };
       
        _serviceMock.Setup(service => service.GetTaskAsync(taskDto.Id)).ReturnsAsync(taskDto);

        // Act
        var result = await _controller.GetTask(taskDto.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnTask = Assert.IsType<ToDoTaskDto>(okResult.Value);
        Assert.Equal(taskDto.Title, returnTask.Title);
    }
    [Fact]
    public async System.Threading.Tasks.Task UpdateTask_ShouldReturnsBadRequest()
    {
        // Arrange
        var taskDto = new ToDoTaskDto { Id = 1, Title = "Task 1" };

        _serviceMock.Setup(service => service.UpdateTaskAsync(taskDto)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateTask(2,taskDto);

        // Assert
        var okResult = Assert.IsType<BadRequestResult>(result);
    }
    public async System.Threading.Tasks.Task UpdateTask_ShouldReturnsOk()
    {
        // Arrange
        var taskDto = new ToDoTaskDto { Id = 1, Title = "Task 1" };

        _serviceMock.Setup(service => service.UpdateTaskAsync(taskDto)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateTask(taskDto.Id, taskDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public async System.Threading.Tasks.Task DeleteTask_ShouldReturnsNotFound()
    {
        // Arrange
        var taskDto = new ToDoTaskDto { Id = 1, Title = "Task 1" };

        _serviceMock.Setup(service => service.DeleteTaskAsync(taskDto.Id)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteTask(taskDto.Id);
        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async System.Threading.Tasks.Task DeleteTask_ShouldReturnsNoContentResult()
    {
        // Arrange
        var taskDto = new ToDoTaskDto { Id = 1, Title = "Task 1" };

        _serviceMock.Setup(service => service.DeleteTaskAsync(taskDto.Id)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteTask(taskDto.Id);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
    }
}