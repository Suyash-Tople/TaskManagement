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
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _project.GetProjectsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            return Ok(await _project.GetProjectByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProject(CreateProjectDto dto)
        {
            var result = await _project.CreateProjectAsync(dto);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto dto)
        {
            var result = await _project.UpdateProjectAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _project.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
