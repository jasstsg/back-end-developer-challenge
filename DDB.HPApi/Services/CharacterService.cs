using DDB.HPApi.Enums;
using DDB.HPApi.Models;
using DDB.HPApi.Repositories.Abstractions;
using DDB.HPApi.Services.Abstractions;

namespace DDB.HPApi.Services
{
    public class CharacterService : ICharacterService
    {
        public readonly ICharacterRepository _repository;

        public CharacterService(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Character> GetCharacter(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Character> DamageCharacter(Guid id, DamageTypes damageType, int value)
        {
            var character = await _repository.GetByIdAsync(id);
            character.Damage(damageType, value);
            return await _repository.UpdateAsync(character);
        }

        public async Task<Character> HealCharacter(Guid id, int value)
        {
            var character = await _repository.GetByIdAsync(id);
            character.Heal(value);
            return await _repository.UpdateAsync(character);
        }

        public async Task<Character> TempHealCharacter(Guid id, int value)
        {
            var character = await _repository.GetByIdAsync(id);
            character.AddTemporaryHitPoints(value);
            return await _repository.UpdateAsync(character);
        }
    }
}
