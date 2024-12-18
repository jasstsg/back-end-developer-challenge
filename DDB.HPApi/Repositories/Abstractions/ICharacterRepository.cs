using DDB.HPApi.Models;

namespace DDB.HPApi.Repositories.Abstractions
{
    public interface ICharacterRepository
    {
        /// <summary>
        /// Retrieves the <see cref="Character"/> associated with <paramref name="id"/> from the database
        /// </summary>
        /// <param name="id">A unique GUID associated with the <see cref="Character"/></param>
        /// <returns>A single <see cref="Character"/></returns>
        Task<Character> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all <see cref="Character"/> objects available from the database
        /// </summary>
        /// <returns>A collection of <see cref="Character"/> objects</returns>
        /// <remarks>A future version will use 'take' and 'skip' parameters to limit the size of the data retrieved</remarks>
        Task<IEnumerable<Character>> GetAllAsync();

        /// <summary>
        /// Updates a <see cref="Character"/> using the id as an identifier for the given <paramref name="character"/>
        /// </summary>
        /// <param name="character">A <see cref="Character"/> object with the new data</param>
        /// <returns>The updated <see cref="Character"/></returns>
        Task<Character> UpdateAsync(Character character);
    }
}
