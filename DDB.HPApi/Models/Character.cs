using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDB.HPApi.Models
{
    public class Character
    {
        private int _maxHitPoints = -1;
        private int _currentHitPoints = 0;
        private int _currentTempHitPoints = 0;

        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int Level { get; set; }

        public int HitPoints
        {
            get
            {
                return _currentHitPoints;
            }
            set
            {
                if (_maxHitPoints < 0)
                {
                    _maxHitPoints = value;
                }
                _currentHitPoints = value;
            }
        }

        public int TempHitPoints
        {
            get
            {
                return _currentTempHitPoints;
            }
            set
            {
                _currentTempHitPoints = value;
            }
        }

        public int MaxHitPoints
        {
            get
            {
                return _maxHitPoints;
            }
        }

        public ICollection<CharacterClass>? Classes { get; set; }

        public Stats? Stats { get; set; }
        public ICollection<Item>? Items { get; set; }

        public ICollection<DefenseType>? Defenses { get; set; }
    }
}
