using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Application.Interfaces
{
    public interface INotificationService
    {
        Task TaskCreated(Guid userId, object task);

        Task TaskUpdated(Guid userId, object task);

        Task TaskDeleted(Guid userId, Guid taskId);
    }
}
