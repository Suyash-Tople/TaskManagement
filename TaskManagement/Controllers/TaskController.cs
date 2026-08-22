using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _taskService.GetAllTaskItems());
        }

        [HttpGet]
        [Route("GetProjectsTasks")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            return Ok(await _taskService.GetTasksByProjectId(projectId));
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewTask(CreateTaskDto  dto)
        {
            var result = await _taskService.CreateTask(dto);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTask(int taskId, UpdateTaskDto dto)
        {
            var result = await _taskService.UpdateTask(taskId, dto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
