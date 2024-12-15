using Microsoft.EntityFrameworkCore;
using DDB.HPApi.Models;

namespace DDB.HPApi.Data
{
    public class CharacterContext : DbContext
    {
        public CharacterContext (DbContextOptions<CharacterContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Character> Characters { get; set; } = default!;
    }
}
