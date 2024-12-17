using DDB.HPApi.Enums;

namespace DDB.HPApi.Models.RequestData
{
    public class Damage
    {
        public DamageTypes DamageType { get; set; }
        public int Value { get; set; }
    }
}
