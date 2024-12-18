using DDB.HPApi.Enums;
using DDB.HPApi.Models;

namespace DDB.HPApi.Services
{
    public static class CharacterServiceHelper
    {
        /// <summary>
        /// Adds temporary hit points to a character
        /// </summary>
        /// <param name="character">The character being given temporary hit points</param>
        /// <param name="tempHitPoints">The amount of temporary hit points being given</param>
        public static void AddTemporaryHitPoints(this Character character, int tempHitPoints)
        {
            character.TempHitPoints = Math.Max(character.TempHitPoints, tempHitPoints);
        }

        /// <summary>
        /// Heals a character
        /// </summary>
        /// <param name="character">The character being healed</param>
        /// <param name="healValue">The amount the character will be healed by</param>
        public static void Heal(this Character character, int healValue)
        {
            character.CurrentHitPoints = Math.Min(character.CurrentHitPoints + healValue, character.HitPoints);
        }

        /// <summary>
        /// Applies damage to a character
        /// </summary>
        /// <param name="character">The character being damaged</param>
        /// <param name="damageType">The type of damage being applied to the character</param>
        /// <param name="damageValue">The amount of damage being done to the character</param>
        public static void Damage(this Character character, DamageTypes damageType, int damageValue)
        {
            damageValue = character.DamageAfterDefenses(damageType, damageValue);

            if (damageValue == 0)
            {
                return; // If no damage is being done exit early
            }

            // Apply the remaining damage to temporary hit points first
            character.TempHitPoints -= damageValue;

            // If temporary hit points fully absorbed the damage, exit early
            if (character.TempHitPoints >= 0)
            {
                return;
            }

            // At this point temporary hit points would be negative, so adding this to hit points reduces it
            // Make sure hit points can't go lower than zero
            character.CurrentHitPoints = Math.Max(character.CurrentHitPoints + character.TempHitPoints, 0);

            // Set temporary hit points to zero
            character.TempHitPoints = 0;
        }

        /// <summary>
        /// This returns the remaining damage to a character after considering their resistances and immunities
        /// </summary>
        /// <param name="character">The character being damaged</param>
        /// <param name="damageType">The type of damage being applied to the character</param>
        /// <param name="damageValue">The amount of damage being done to the character</param>
        /// <returns>The remaining damage amount</returns>
        private static int DamageAfterDefenses(this Character character, DamageTypes damageType, int damageValue)
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
                        // Character is immune, set the damage to 0
                        return 0;
                    }

                    else if (def.Defense.Equals(DefensePotency.Resistance))
                    {
                        // Character is resistant, cut the damage in half (rounding down for odd numbers)
                        return damageValue / 2;
                    }
                }
            }
            return damageValue;
        }
    }
}
