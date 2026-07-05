using FluentAssertions;
using Moq;
using TaskTracker.Application.DTOs.Task;
using TaskTracker.Application.Entities;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Services;
using Xunit;

namespace TaskTracker.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repository;

        private readonly Mock<INotificationService> _notification;

        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _repository = new Mock<ITaskRepository>();

            _notification = new Mock<INotificationService>();

            _service = new TaskService(
                _repository.Object,
                _notification.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Task()
        {
            var request = new CreateTaskRequest
            {
                Title = "Write Tests",
                Description = "Task Service",
                DueDate = DateTime.UtcNow.AddDays(2)
            };

            var userId = Guid.NewGuid();

            var result = await _service.CreateAsync(
                request,
                userId);

            result.Should().NotBeNull();

            result.Title.Should().Be(request.Title);

            _repository.Verify(x =>
                x.AddAsync(It.IsAny<TaskItem>()),
                Times.Once);

            _repository.Verify(x =>
                x.SaveChangesAsync(),
                Times.Once);

            _notification.Verify(x =>
                x.TaskCreated(userId, It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public async Task GetById_Should_Return_Task()
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Sample",
                OwnerId = Guid.NewGuid(),
                Owner = new User
                {
                    Name = "John"
                }
            };

            _repository.Setup(x =>
                x.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            var result = await _service.GetByIdAsync(
                task.Id,
                task.OwnerId,
                false);

            result.Should().NotBeNull();

            result!.Title.Should().Be("Sample");
        }

        [Fact]
        public async Task GetById_Should_Throw_When_NotFound()
        {
            _repository.Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TaskItem?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.GetByIdAsync(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    false));
        }

        [Fact]
        public async Task Update_Should_Throw_When_NotOwner()
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                OwnerId = Guid.NewGuid()
            };

            _repository.Setup(x =>
                x.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _service.UpdateAsync(
                    task.Id,
                    new UpdateTaskRequest(),
                    Guid.NewGuid(),
                    false));
        }

        [Fact]
        public async Task Delete_Should_Remove_Task()
        {
            var owner = Guid.NewGuid();

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                OwnerId = owner
            };

            _repository.Setup(x =>
                x.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            await _service.DeleteAsync(
                task.Id,
                owner,
                false);

            _repository.Verify(x =>
                x.Delete(It.IsAny<TaskItem>()),
                Times.Once);

            _repository.Verify(x =>
                x.SaveChangesAsync(),
                Times.Once);

            _notification.Verify(x =>
                x.TaskDeleted(owner, task.Id),
                Times.Once);
        }
    }
}
