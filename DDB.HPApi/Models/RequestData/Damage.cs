using DDB.HPApi.Enums;

namespace DDB.HPApi.Models.RequestData
{
    public class Damage
    {
        /// <summary>
        /// Type of damage being applied
        /// </summary>
        public DamageTypes DamageType { get; set; }

        /// <summary>
        /// The amount of damage being applied
        /// </summary>
        public int Value { get; set; }
    }
}
