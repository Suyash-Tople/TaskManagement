using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _project;
        public ProjectController(IProjectService project)
        {
            _project = project;
        }

        [HttpGet]
        [Route("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _project.GetProjectsAsync());
        }

        [HttpGet]
        [Route("GetProjectById/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            return Ok(await _project.GetProjectByIdAsync(id));
        }

        [HttpPost]
        [Route("AddNewProject")]
        public async Task<IActionResult> AddNewProject(CreateProjectDto dto)
        {
            var result = await _project.CreateProjectAsync(dto);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProject")]
        public async Task<IActionResult> UpdateProject(int projectId, int managerId, ProjectDto dto)
        {
            var result = await _project.UpdateProjectAsync(projectId, managerId, dto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteProject")]
        public async Task<IActionResult> DeleteProject(int projectId, int managerId)
        {
            await _project.DeleteProjectAsync(projectId, managerId);
            return NoContent();
        }

        [HttpPut]
        [Route("ChangeProjectManager")]
        public async Task<IActionResult> ChangeProjectManager(int projectId, int currentManagerId, int managerId)
        {
            var result = await _project.ChangeProjectManager(projectId, currentManagerId, managerId);
            return Ok(result);
        }
    }
}
