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
        [Route("GetAllUsersSkills")]
        public async Task<IActionResult> GetAllUsersSkills()
        {
            return Ok(await _userSkills.GetAllUsersSkills());
        }

        [HttpGet]
        [Route("GetUserSkillsByUserId")]
        public async Task<IActionResult> GetAllUserSkillsByUserId(int id)
        {
            return Ok(await _userSkills.GetUserSkillsById(id));
        }

        [HttpPost]
        [Route("AddSkillToUser")]
        public async Task<IActionResult> AddSkillToUser(AddSkillToUserDto dto)
        {
            var result = await _userSkills.AddSkillToUser(dto);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateUseSkill")]
        public async Task<IActionResult> UpdateUserSkill(int userId, UpdateUserSkillDto dto)
        {
            var result = await _userSkills.UpdateUserSkill(userId, dto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUserSkill")]
        public async Task<IActionResult> DeleteUserSkill(int id)
        {
            await _userSkills.DeleteUserSkill(id);
            return NoContent();
        }
    }
}
