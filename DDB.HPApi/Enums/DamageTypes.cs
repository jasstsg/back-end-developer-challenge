using System.Text.Json.Serialization;

namespace DDB.HPApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]   
    public enum DamageTypes
    {
        Bludgeoning,
        Piercing,
        Slashing,
        Fire,
        Cold,
        Acid,
        Thunder,
        Lightning,
        Poison,
        Radiant,
        Necrotic,
        Psychic,
        Force
    }
}
