using DDB.HPApi.Enums;
using DDB.HPApi.Models;

namespace DDB.HPApi.Services.Abstractions
{
    public interface ICharacterService
    {
        /// <summary>
        /// Service method providing a single character with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <returns>A single <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
        Task<Character> GetCharacter(Guid id);

        /// <summary>
        /// Service method that provides all characters
        /// </summary>
        /// <returns>A collection of <see cref="Character"/> objects as an <see cref="IActionResult"/></returns>
        Task<IEnumerable<Character>> GetCharacters();

        /// <summary>
        /// Service method that applies <paramref name="damage"/> to a <see cref="Character"/> associated with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <param name="damage">A <see cref="Damage"/> object containing the type and amount of damage applied</param>
        /// <returns>The updated <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
        Task<Character> DamageCharacter(Guid id, DamageTypes damageType, int value);

        /// <summary>
        /// Service method that heals the <see cref="Character"/> associated with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <param name="heal">A <see cref="Heal"/> object containing the amount of healing applied</param>
        /// <returns>The updated <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
        Task<Character> HealCharacter(Guid id, int value);

        /// <summary>
        /// Service method that adds temporary hit points to the <see cref="Character"/> associated with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <param name="tempHeal">A <see cref="Heal"/> object containing the amount of temporary healing applied</param>
        /// <returns>The updated <see cref="Character" /> object as an <see cref="IActionResult"/></returns>
        Task<Character> TempHealCharacter(Guid id, int value);
    }
}
