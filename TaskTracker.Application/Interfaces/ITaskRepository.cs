using TaskTracker.Application.DTOs.Task;
using TaskTracker.Application.Entities;

namespace TaskTracker.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);

        Task<List<TaskItem>> GetTasksAsync(
        TaskQueryParameters query,
        Guid currentUserId,
        bool isAdmin);

        Task<List<TaskItem>> GetByOwnerIdAsync(Guid ownerId);

        Task AddAsync(TaskItem task);

        void Update(TaskItem task);

        void Delete(TaskItem task);

        Task SaveChangesAsync();
    }
}
