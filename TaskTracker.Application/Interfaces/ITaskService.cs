using TaskTracker.Application.DTOs.Task;

namespace TaskTracker.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateAsync(
            CreateTaskRequest request,
            Guid currentUserId);

        Task<IEnumerable<TaskResponse>> GetAllAsync(
            TaskQueryParameters query,
            Guid currentUserId,
            bool isAdmin);

        Task<TaskResponse?> GetByIdAsync(
            Guid id,
            Guid currentUserId,
            bool isAdmin);

        Task UpdateAsync(
            Guid id,
            UpdateTaskRequest request,
            Guid currentUserId,
            bool isAdmin);

        Task DeleteAsync(
            Guid id,
            Guid currentUserId,
            bool isAdmin);
    }
}
