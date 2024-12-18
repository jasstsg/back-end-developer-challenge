using DDB.HPApi.Controllers.Abstractions;
using DDB.HPApi.Exceptions;
using DDB.HPApi.Models;
using DDB.HPApi.Models.RequestData;
using DDB.HPApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DDB.HPApi.Controllers
{
    /// <summary>
    /// A controller exposing the API for external applications
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : CustomControllerBase
    {
        private readonly ICharacterService _service;

        public CharactersController(ICharacterService service, ILogger<CharactersController> logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a single character with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <returns>A single <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCharacter(Guid id)
        {
            try
            {
                var result = await _service.GetCharacter(id);
                return OkResponse(result, $"Retrieving character '{id}'.");
            }
            catch (CharacterWithIdNotFound ex)
            {
                return NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all characters
        /// </summary>
        /// <returns>A collection of <see cref="Character"/> objects as an <see cref="IActionResult"/></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCharacters()
        {
            // Ideally we would actually limit the number of characters being returned in case there was an enormous amount
            // You made doing this by introducing 'take' and 'skip' parameters in the request
            try
            {
                var result = await _service.GetCharacters();
                return OkResponse(result, "Retrieving all characters");
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        /// <summary>
        /// Applies <paramref name="damage"/> to a <see cref="Character"/> associated with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <param name="damage">A <see cref="Damage"/> object containing the type and amount of damage applied</param>
        /// <returns>The updated <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
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
                
                return OkResponse(result, $"Attempting to deal '{damage.Value}' '{damage.DamageType}' damage to character ({id}).");
            }
            catch (CharacterWithIdNotFound ex)
            {
                return NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        /// <summary>
        /// Heals the <see cref="Character"/> associated with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <param name="heal">A <see cref="Heal"/> object containing the amount of healing applied</param>
        /// <returns>The updated <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
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
                
                return OkResponse(result, $"Healing character ({id}) up to '{heal.Value}' hit points.");
            }
            catch (CharacterWithIdNotFound ex)
            {
                return NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        /// <summary>
        /// Adds temporary hit points to the <see cref="Character"/> associated with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <param name="tempHeal">A <see cref="Heal"/> object containing the amount of temporary healing applied</param>
        /// <returns>The updated <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
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
                
                return OkResponse(result, $"Adding '{tempHeal.Value}' temporary hit points to character ({id}).");
            }
            catch (CharacterWithIdNotFound ex)
            {
                return NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }
    }
}
