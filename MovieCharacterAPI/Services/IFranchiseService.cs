using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Services
{
	public interface IFranchiseService : IService<Franchise>
	{
		/// <summary>
		/// Updates movies in a franchise by adding Ids in the movies list to the franchise movie list.
		/// </summary>
		/// <param name="franchiseId"></param>
		/// <param name="movies"></param>
		Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movies);
		/// <summary>
		/// Gets all movies that are in the specific franchise Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns a list of movies in the franchise.</returns>
		Task<IEnumerable<Movie>> GetMoviesInFranchiseAsync(int id);
		/// <summary>
		/// Gets all characters that are in the specific franchise Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns a list of characters in the franchise.</returns>
		Task<IEnumerable<Character>> GetCharactersInFranchiseAsync(int id);
	}
}
