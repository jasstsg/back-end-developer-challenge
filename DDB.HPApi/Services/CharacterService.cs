using DDB.HPApi.Enums;
using DDB.HPApi.Models;
using DDB.HPApi.Repositories.Abstractions;
using DDB.HPApi.Services.Abstractions;
using Newtonsoft.Json.Linq;

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
            return await _repository.GetAll();
        }

        private int ApplyDamageReduction(Character character, DamageTypes damageType, int damageValue)
        {
            // Exit early if defenses can't be retrieved
            if (character?.Defenses == null)
            {
                return damageValue;
            }

            // Check for any immunities or resistances
            foreach (DefenseType def in character.Defenses)
            {
                if (def.Type.Equals(damageType))
                {
                    if (def.Defense.Equals(DefensePotency.Immunity))
                    {
                        return 0; // Character is immune, set the damage to 0
                    }

                    else if (def.Defense.Equals(DefensePotency.Resistance))
                    {
                        return damageValue / 2; // Character is resistant, cut the damage in half (rounding down for odd numbers)
                    }
                }
            }
            return damageValue;
        }

        public async Task<Character> DamageCharacter(Guid id, DamageTypes damageType, int value)
        {
            var character = await _repository.GetByIdAsync(id);
            value = ApplyDamageReduction(character, damageType, value);
            if (value == 0)
            {
                return character; // If no damage is being done exit early
            }

            // Apply the remaining damage to temporary hit points first
            character.TempHitPoints -= value;

            // If temporary hit points fully absorbed the damage, update the character and exit
            if (character.TempHitPoints >= 0)
            {
                return await _repository.UpdateAsync(character);
            }

            // At this point temporary hit points would be negative, so adding this to hit points reduces it
            // Make sure hit points can't go lower than zero
            // Set temporary hit points to zero
            character.HitPoints = Math.Max(character.HitPoints + character.TempHitPoints, 0);
            character.TempHitPoints = 0;

            return await _repository.UpdateAsync(character);
        }

        public async Task<Character> HealCharacter(Guid id, int value)
        {
            var character = await _repository.GetByIdAsync(id);
            character.HitPoints = Math.Min(character.HitPoints + value, character.MaxHitPoints);
            return await _repository.UpdateAsync(character);
        }

        public async Task<Character> TempHealCharacter(Guid id, int value)
        {
            var character = await _repository.GetByIdAsync(id);
            character.TempHitPoints += value;
            return await _repository.UpdateAsync(character);
        }
    }
}
