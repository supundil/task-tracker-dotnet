using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Application.Enums;

namespace TaskTracker.Application.DTOs.Task
{
    public class CreateTaskRequest
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;

        public DateTime? DueDate { get; set; }
    }
}
