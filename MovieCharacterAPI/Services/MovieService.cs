using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Data;
using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Services
{
	public class MovieService : IMovieService
	{

		private readonly MovieDbContext _context;

		public MovieService(MovieDbContext context)
		{
			_context = context;
		}

		public async Task UpdateMovieCharacters(int movieId, List<int> characters)
		{

			Movie movieToUpdateCharacters = await _context.Movies.
				Include(m => m.Characters).
				Where(m => m.Id == movieId).FirstAsync();

			List<Character> characterList = new();

			foreach (int characterId in characters)
			{
				Character character = await _context.Characters.FindAsync(characterId);
				if (character == null)
				{
					throw new KeyNotFoundException();
				}
				characterList.Add(character);
			}
			movieToUpdateCharacters.Characters = characterList;
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Character>> GetCharactersInMovieAsync(int movieId)
		{
			Movie movie = await GetById(movieId);
			return movie.Characters.ToList();
		}

		public async Task<Movie> Add(Movie movie)
		{
			_context.Movies.Add(movie);
			await _context.SaveChangesAsync();
			return movie;
		}

		public bool Exists(int id)
		{
			return _context.Movies.Any(e => e.Id == id);
		}

		public async Task Delete(int id)
		{
			Movie movie = await _context.Movies.FindAsync(id);
			_context.Movies.Remove(movie);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Movie>> GetAll()
		{
			return await _context.Movies
				.Include(m => m.Characters)
				.ToListAsync<Movie>();
		}

		public async Task<Movie> GetById(int id)
		{
			if (!Exists(id)) return null;
			return await _context.Movies.Include(m => m.Characters).Where(m => m.Id == id).FirstAsync();
		}

		public async Task Update(Movie movie)
		{
			_context.Entry(movie).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
