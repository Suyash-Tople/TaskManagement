using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            return Ok(await _skillService.GetAllSkills());
        }

        [HttpGet]
        [Route("GetSkillById")]
        public async Task<IActionResult> GetSkillById(int id)
        {
            return Ok(await _skillService.GetSkillById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSkill(CreateSkillDto dto)
        {
            var result = await _skillService.AddSkill(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSkill(int id, UpdateSkillDto dto)
        {
            var result = await _skillService.UpdateSkill(id, dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            await _skillService.DeleteSkill(id);
            return NoContent();
        }
    }
}
