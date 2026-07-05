using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Application.Enums;

namespace TaskTracker.Application.DTOs.Task
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Enums.TaskStatus Status { get; set; }

        public DateTime? DueDate { get; set; }

        public Guid OwnerId { get; set; }

        public string OwnerName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
