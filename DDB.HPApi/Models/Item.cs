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

        [InverseProperty("Item")]
        public ItemModifier? Modifier { get; set; }

        [JsonIgnore]
        [ForeignKey("CharacterId")]
        public Character? Character { get; set; }
    }

}
