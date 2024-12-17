using DDB.HPApi.Controllers.Abstractions;
using DDB.HPApi.Enums;
using DDB.HPApi.Models.RequestData;
using DDB.HPApi.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DDB.HPApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CharactersController : CustomControllerBase
    {
        private readonly ICharacterService _service;

        public CharactersController(ICharacterService service, ILogger<CharactersController> logger) : base(logger)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCharacter(Guid id)
        {
            try
            {
                var result = await _service.GetCharacter(id);
                return OkResult(result, $"Retrieving character '{id}'.");
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCharacters()
        {
            try
            {
                var result = await _service.GetCharacters();
                return OkResult(result, "Retrieving all characters");
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        [HttpPost]
        [Route("{id}/damage")]
        public async Task<IActionResult> DamageCharacter(Guid id, [FromBody] Damage damage)
        {
            try
            {
                if (damage.Value < 0)
                {
                    return BadRequestResponse("Damage value must be a positive integer");
                }
                var result = await _service.DamageCharacter(id, damage.DamageType, damage.Value);
                return OkResult(result, $"Attempting to deal '{damage.Value}' '{damage.DamageType}' damage to character ({id}).");
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        [HttpPost]
        [Route("{id}/heal")]
        public async Task<IActionResult> HealCharacter(Guid id, [FromBody] Heal heal)
        {
            try
            {
                if (heal.Value < 0)
                {
                    return BadRequestResponse("Heal value must be a positive integer");
                }
                var result = await _service.HealCharacter(id, heal.Value);
                return OkResult(result, $"Healing character ({id}) up to '{heal.Value}' hit points.");
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        [HttpPost]
        [Route("{id}/temp-heal")]
        public async Task<IActionResult> TempHealCharacter(Guid id, [FromBody] Heal tempHeal)
        {
            try
            {
                if (tempHeal.Value < 0)
                {
                    return BadRequestResponse("Temporary heal value must be a positive integer");
                }
                var result = await _service.TempHealCharacter(id, tempHeal.Value);
                return OkResult(result, $"Adding '{tempHeal.Value}' temporary hit points to character ({id}).");
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }
    }
}
