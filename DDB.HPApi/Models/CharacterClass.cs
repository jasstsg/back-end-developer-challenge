using DDB.HPApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DDB.HPApi.Models
{
    public class CharacterClass
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int HitDiceValue { get; set; }

        public int ClassLevel { get; set; }

        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Guid CharacterId { get; set; }

        [JsonIgnore]
        public Character? Character { get; set; }
    }
}
