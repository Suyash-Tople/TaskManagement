using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Models.Enums;
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
        [Route("AllTasksOfProject")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            return Ok(await _taskService.GetTasksByProjectId(projectId));
        }

        [HttpGet]
        [Route("GetAllTasksByUserId")]
        public async Task<Object> GetAllTasksByUserId(int userId)
        {
            return Ok(await _taskService.GetAllTasksByUserId(userId));
        }

        [HttpPost]
        [Route("CreateNewTask")]
        public async Task<IActionResult> CreateNewTask(TaskItemDto dto)
        {
            var result = await _taskService.CreateTask(dto);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask(int taskId, TaskItemDto dto)
        {
            var result = await _taskService.UpdateTask(taskId, dto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteTask")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }

        [HttpPatch]
        [Route("UpdateTaskStatus")]
        public async Task<Object> UpdateTaskStatus(int taskId, ProjectTaskStatus status)
        {
            await _taskService.UpdateTaskStatus(taskId, status);
            return NoContent();
        }
    }
}
