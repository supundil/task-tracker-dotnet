using TaskTracker.Application.DTOs.Task;
using TaskTracker.Application.Entities;
using TaskTracker.Application.Interfaces;

namespace TaskTracker.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly INotificationService _notificationService;

        public TaskService(ITaskRepository taskRepository,
            INotificationService notificationService)
        {
            _taskRepository = taskRepository;
            _notificationService = notificationService;
        }

        public async Task<TaskResponse> CreateAsync(
        CreateTaskRequest request,
        Guid currentUserId)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),

                Title = request.Title,

                Description = request.Description,

                Status = request.Status,

                DueDate = request.DueDate.HasValue
                ? DateTime.SpecifyKind(request.DueDate.Value, DateTimeKind.Utc)
                : null,
                OwnerId = currentUserId,

                CreatedAt = DateTime.UtcNow,

                UpdatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(task);

            await _taskRepository.SaveChangesAsync();

            await _notificationService.TaskCreated(
            currentUserId,
            task);

            return new TaskResponse
            {
                Id = task.Id,

                Title = task.Title,

                Description = task.Description,

                Status = task.Status,

                DueDate = task.DueDate,

                OwnerId = task.OwnerId,

                CreatedAt = task.CreatedAt
            };
        }

        public async Task<IEnumerable<TaskResponse>> GetAllAsync(
        TaskQueryParameters query,
        Guid currentUserId,
        bool isAdmin)
        {
            var tasks =
                await _taskRepository.GetTasksAsync(
                    query,
                    currentUserId,
                    isAdmin);

            return tasks.Select(task => new TaskResponse
            {
                Id = task.Id,

                Title = task.Title,

                Description = task.Description,

                Status = task.Status,

                DueDate = task.DueDate,

                OwnerId = task.OwnerId,

                OwnerName = task.Owner.Name,

                CreatedAt = task.CreatedAt
            });
        }

        public async Task<TaskResponse?> GetByIdAsync(
        Guid id,
        Guid currentUserId,
        bool isAdmin)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Task not found.");

            // Ownership Check
            if (!isAdmin && task.OwnerId != currentUserId)
                throw new UnauthorizedAccessException("You are not allowed to access this task.");

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                OwnerId = task.OwnerId,
                OwnerName = task.Owner.Name,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task UpdateAsync(
        Guid id,
        UpdateTaskRequest request,
        Guid currentUserId,
        bool isAdmin)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Task not found.");

            // RBAC
            if (!isAdmin && task.OwnerId != currentUserId)
                throw new UnauthorizedAccessException("You cannot update this task.");

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.DueDate = request.DueDate.HasValue
                ? DateTime.SpecifyKind(request.DueDate.Value, DateTimeKind.Utc)
                : null;
            task.UpdatedAt = DateTime.UtcNow;

            _taskRepository.Update(task);

            await _taskRepository.SaveChangesAsync();

            await _notificationService.TaskUpdated(
            task.OwnerId,
            task);
        }

        public async Task DeleteAsync(
        Guid id,
        Guid currentUserId,
        bool isAdmin)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Task not found.");

            // RBAC
            if (!isAdmin && task.OwnerId != currentUserId)
                throw new UnauthorizedAccessException("You cannot delete this task.");

            var taskId = task.Id;

            _taskRepository.Delete(task);

            await _taskRepository.SaveChangesAsync();

            await _notificationService.TaskDeleted(
            task.OwnerId,
            taskId);
        }
    }
}
