using System.ComponentModel.DataAnnotations;

namespace DDB.HPApi.Models
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int Level { get; set; }

        public int HitPoints { get; set; }  // Max hit points

        public int TempHitPoints { get; set; }

        public int CurrentHitPoints { get; set; }

        public ICollection<CharacterClass>? Classes { get; set; }

        public Stats? Stats { get; set; }
        public ICollection<Item>? Items { get; set; }

        public ICollection<DefenseType>? Defenses { get; set; }
    }
}
