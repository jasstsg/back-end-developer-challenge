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

        [JsonIgnore]
        [ForeignKey("CharacterId")]
        public Character? Character { get; set; }
    }
}
