using Microsoft.EntityFrameworkCore;
using DDB.HPApi.Models;

namespace DDB.HPApi.Data
{
    public class CharacterContext : DbContext
    {
        public CharacterContext (DbContextOptions<CharacterContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; } = default!;
        public DbSet<CharacterClass> CharacterClasses { get; set; } = default!;
        public DbSet<Stats> Stats { get; set; } = default!;
        public DbSet<DefenseType> Defenses { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<ItemModifier> ItemModifiers { get; set; } = default!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
