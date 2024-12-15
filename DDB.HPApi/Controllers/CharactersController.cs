using Microsoft.AspNetCore.Mvc;
using DDB.HPApi.Enums;
using DDB.HPApi.Services.Abstractions;

namespace DDB.HPApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CharactersController : Controller
    {
        private readonly ICharacterService _service;

        public CharactersController(ICharacterService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetCharacter(Guid id)
        {
            var result = await _service.GetCharacter(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("")]

        public async Task<IActionResult> GetCharacters()
        {
            var result = await _service.GetCharacters();
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}/damage")]
        public async Task<IActionResult> DamageCharacter(Guid id, DamageTypes damageType, int value)
        {
            var result = await _service.DamageCharacter(id, damageType, value);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}/heal")]
        public async Task<IActionResult> HealCharacter(Guid id, int value)
        {
            var result = await _service.HealCharacter(id, value);
            return Ok(result);
        }


        [HttpPut]
        [Route("{id}/temp-heal")]
        public async Task<IActionResult> TempHealCharacter(Guid id, int value)
        {
            var result = await _service.TempHealCharacter(id, value);
            return Ok(result);
        }
    }
}
