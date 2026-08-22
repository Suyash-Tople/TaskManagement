using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkillsController : ControllerBase
    {
        private readonly IUserSkillsService _userSkills;
        public UserSkillsController(IUserSkillsService userSkills)
        {
            _userSkills = userSkills;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersSkills()
        {
            return Ok(await _userSkills.GetAllUsersSkills());
        }

        [HttpGet]
        [Route("GetAllUserSkillsByUserId")]
        public async Task<IActionResult> GetAllUserSkillsByUserId(int id)
        {
            return Ok(await _userSkills.GetUserSkillsById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddSkillToUser(AddSkillToUserDto dto)
        {
            var result = await _userSkills.AddSkillToUser(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserSkill(int id, UpdateUserSkillDto dto)
        {
            var result = await _userSkills.UpdateUserSkill(id, dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserSkill(int id)
        {
            await _userSkills.DeleteUserSkill(id);
            return NoContent();
        }
    }
}
