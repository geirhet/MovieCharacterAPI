using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Services
{
	public interface IMovieService : IService<Movie>
	{
		/// <summary>
		/// Updates characters in a movie by adding Ids in the characters list to the movie character list.
		/// </summary>
		/// <param name="movieId"></param>
		/// <param name="characters"></param>
		Task UpdateMovieCharacters(int movieId, List<int> characters);
		/// <summary>
		/// Gets all characters that are in the specific movie Id.
		/// </summary>
		/// <param name="movieId"></param>
		/// <returns>Returns a list of characters in the movie.</returns>
		Task<IEnumerable<Character>> GetCharactersInMovieAsync(int movieId);
	}
}
