using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DDB.HPApi.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public ItemModifier? Modifier { get; set; }

        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Guid CharacterId { get; set; }

        [JsonIgnore]
        public Character? Character { get; set; }
    }

}
