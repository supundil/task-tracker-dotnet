using Microsoft.AspNetCore.SignalR;
using TaskTracker.Application.Interfaces;

namespace TaskTracker.Infrastructure.SignalR
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<TaskHub> _hub;

        public NotificationService(IHubContext<TaskHub> hub)
        {
            _hub = hub;
        }

        public async Task TaskCreated(Guid userId, object task)
        {
            await _hub.Clients
                .User(userId.ToString())
                .SendAsync("TaskCreated", task);
        }

        public async Task TaskUpdated(Guid userId, object task)
        {
            await _hub.Clients
                .User(userId.ToString())
                .SendAsync("TaskUpdated", task);
        }

        public async Task TaskDeleted(Guid userId, Guid taskId)
        {
            await _hub.Clients
                .User(userId.ToString())
                .SendAsync("TaskDeleted", taskId);
        }
    }
}
