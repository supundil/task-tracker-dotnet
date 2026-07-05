using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Application.Enums;

namespace TaskTracker.Application.DTOs.Task
{
    public class TaskQueryParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public Enums.TaskStatus? Status { get; set; }

        public string? Search { get; set; }
    }
}
