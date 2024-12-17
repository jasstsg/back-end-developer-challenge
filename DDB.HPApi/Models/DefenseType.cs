using DDB.HPApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DDB.HPApi.Models
{
    public class DefenseType
    {
        [Key]
        public Guid Id { get; set; }

        public DamageTypes Type { get; set; }

        public DefensePotency Defense { get; set; }

        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Guid CharacterId { get; set; }

        [JsonIgnore]
        public Character? Character { get; set; }
    }
}
