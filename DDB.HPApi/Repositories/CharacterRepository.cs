using DDB.HPApi.Data;
using DDB.HPApi.Exceptions;
using DDB.HPApi.Models;
using DDB.HPApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DDB.HPApi.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly CharacterContext _context;

        public CharacterRepository(CharacterContext context)
        {
            _context = context;
        }

        public async Task<Character> GetByIdAsync(Guid id)
        {
            var result = await _context.Characters
                .Include(c => c.Stats)
                .Include(c => c.Classes)
                .Include(c => c.Defenses)
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id.Equals(id));

            if (result == null)
            {
                throw new CharacterWithIdNotFound(id);
            }

            return result;
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return await _context.Characters
                .Include(c => c.Stats)
                .Include(c => c.Classes)
                .Include(c => c.Defenses)
                .Include(c => c.Items)
                .ToListAsync();
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            var result = await _context.Characters.FindAsync(character.Id);
            if (result == null)
            {
                throw new CharacterWithIdNotFound(character.Id);
            }

            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
