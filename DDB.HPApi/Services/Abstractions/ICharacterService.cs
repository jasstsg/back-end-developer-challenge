using DDB.HPApi.Enums;
using DDB.HPApi.Models;

namespace DDB.HPApi.Services.Abstractions
{
    public interface ICharacterService
    {
        Task<Character> GetCharacter(Guid id);
        Task<IEnumerable<Character>> GetCharacters();
        Task<Character> DamageCharacter(Guid id, DamageTypes damageType, int value);
        Task<Character> HealCharacter(Guid id, int value);
        Task<Character> TempHealCharacter(Guid id, int value);
    }
}
