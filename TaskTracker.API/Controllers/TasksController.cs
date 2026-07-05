using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.DTOs.Task;
using TaskTracker.Application.Interfaces;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private Guid CurrentUserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private bool IsAdmin =>
            User.IsInRole("Admin");

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskRequest request)
        {
            var task = await _taskService.CreateAsync(
                request,
                CurrentUserId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = task.Id },
                task);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery] TaskQueryParameters query)
        {
            var tasks = await _taskService.GetAllAsync(
                query,
                CurrentUserId,
                IsAdmin);

            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetByIdAsync(
                id,
                CurrentUserId,
                IsAdmin);

            return Ok(task);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
        Guid id,
        UpdateTaskRequest request)
        {
            await _taskService.UpdateAsync(
                id,
                request,
                CurrentUserId,
                IsAdmin);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.DeleteAsync(
                id,
                CurrentUserId,
                IsAdmin);

            return NoContent();
        }

    }
}
