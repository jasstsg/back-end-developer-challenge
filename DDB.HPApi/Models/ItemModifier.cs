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
        [Required]
        public int Value { get; set; }

        public Guid ItemId { get; set; }

        [JsonIgnore]
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
    }
}
