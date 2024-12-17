using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DDB.HPApi.Models
{
    public class Stats
    {
        [Key]
        public Guid Id { get; set; }

        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Constitution { get; set; }

        public int Intelligence { get; set; }

        public int Wisdom { get; set; }

        public int Charisma { get; set; }

        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Guid CharacterId { get; set; }

        [JsonIgnore]
        public Character? Character { get; set; }
    }
}
