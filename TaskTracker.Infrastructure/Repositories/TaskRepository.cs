using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.DTOs.Task;
using TaskTracker.Application.Entities;
using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Persistence;

namespace TaskTracker.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Delete(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }

        public async Task<List<TaskItem>> GetTasksAsync(
        TaskQueryParameters query,
        Guid currentUserId,
        bool isAdmin)
        {
            IQueryable<TaskItem> tasks =
                _context.Tasks.Include(x => x.Owner);

            if (!isAdmin)
            {
                tasks = tasks.Where(x => x.OwnerId == currentUserId);
            }

            if (query.Status.HasValue)
            {
                tasks = tasks.Where(x => x.Status == query.Status);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                tasks = tasks.Where(x =>
                    x.Title.Contains(query.Search));
            }

            return await tasks
                .OrderByDescending(x => x.CreatedAt)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Owner)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskItem>> GetByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Tasks
                .Include(t => t.Owner)
                .Where(t => t.OwnerId == ownerId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
