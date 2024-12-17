using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DDB.HPApi.Models
{

    public class ItemModifier
    {
        [Key]
        public Guid Id { get; set; }

        public string? AffectedObject { get; set; }

        public string? AffectedValue { get; set; }

        public int Value { get; set; }

        [ForeignKey("ItemId")]
        [JsonIgnore]
        public Guid ItemId { get; set; }

        [JsonIgnore]
        public Item? Item { get; set; }
    }
}
