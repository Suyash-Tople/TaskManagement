//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel;
//using TaskManagement.Models;

//namespace TaskManagement.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProjectController : ControllerBase
//    {
//        static List<Project> projects = new List<Project>();
//        [HttpPost]
//        public async Task<IActionResult> createProject(Project _project)
//        {
//            projects.Add(_project);
//            return Ok(projects);
//        }
//        [HttpGet]
//        public async Task<IActionResult> getProject(int id)
//        {
//            var projectExists = projects.FirstOrDefault(x => x.Id == id);
//            if (projectExists == null)
//            {
//                return NotFound("Project Does not exists");
//            }
//            else
//            {
//                return Ok(projectExists);
//            }
//        }
//    }
//}
