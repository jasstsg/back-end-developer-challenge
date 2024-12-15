using DDB.HPApi.Models;

namespace DDB.HPApi.Repositories.Abstractions
{
    public interface ICharacterRepository
    {
        Task<Character> GetByIdAsync(Guid id);
        Task<IEnumerable<Character>> GetAll();
        Task<Character> UpdateAsync(Character character);
    }
}
