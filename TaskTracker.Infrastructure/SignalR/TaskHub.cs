using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TaskTracker.Infrastructure.SignalR
{
    [Authorize]
    public class TaskHub : Hub
    {
    }
}
