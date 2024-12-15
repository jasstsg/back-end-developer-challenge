using System.Text.Json.Serialization;

namespace DDB.HPApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DefensePotency
    {
        None,
        Resistance,
        Immunity
    }
}
