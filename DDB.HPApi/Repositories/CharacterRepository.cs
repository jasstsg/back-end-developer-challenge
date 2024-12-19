using DDB.HPApi.Data;
using DDB.HPApi.Exceptions;
using DDB.HPApi.Models;
using DDB.HPApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DDB.HPApi.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly CharacterContext _context;
        private readonly IMemoryCache _memoryCache;

        public CharacterRepository(CharacterContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Character> GetByIdAsync(Guid id)
        {
            string cacheKey = $"CACHE_CHARACTER_{id}";
            Character? character;
            if (!_memoryCache.TryGetValue(cacheKey, out character))
            {
                character = await _context.Characters
                    .Include(c => c.Stats)
                    .Include(c => c.Classes)
                    .Include(c => c.Defenses)
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.Id.Equals(id));


                if (character == null)
                {
                    throw new CharacterWithIdNotFound(id);
                }

                _memoryCache.Set(cacheKey, character);
            }

            return character!;
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            string cacheKey = "CACHE_CHARACTER_ALL";
            IEnumerable<Character>? characters;
            if (!_memoryCache.TryGetValue(cacheKey, out characters))
            {
                characters = await _context.Characters
                    .Include(c => c.Stats)
                    .Include(c => c.Classes)
                    .Include(c => c.Defenses)
                    .Include(c => c.Items)
                    .ToListAsync();
                _memoryCache.Set(cacheKey, characters);
            }
            return characters!;
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            var result = await this.GetByIdAsync(character.Id);

            string cacheKey = $"CACHE_CHARACTER_{character.Id}";
            _memoryCache.Set(cacheKey, character);

            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
