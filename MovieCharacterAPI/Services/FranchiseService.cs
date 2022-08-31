using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Data;
using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Services
{
	public class FranchiseService : IFranchiseService
	{
		private readonly MovieDbContext _context;
		private readonly IMovieService _movieService;

		public FranchiseService(MovieDbContext context, IMovieService movieService)
		{
			_context = context;
			_movieService = movieService;
		}

		public async Task<Franchise> Add(Franchise franchise)
		{
			_context.Franchises.Add(franchise);
			await _context.SaveChangesAsync();
			return franchise;

		}

		public async Task Delete(int id)
		{
			Franchise franchise = await _context.Franchises.FindAsync(id);
			_context.Franchises.Remove(franchise);
			await _context.SaveChangesAsync();
		}

		public bool Exists(int id)
		{
			return _context.Franchises.Any(e => e.Id == id);
		}

		public async Task<IEnumerable<Franchise>> GetAll()
		{
			return await _context.Franchises.Include(f => f.Movies).ToListAsync<Franchise>();
		}

		public async Task<Franchise> GetById(int id)
		{
			if (!Exists(id)) return null;
			return await _context.Franchises.Include(f => f.Movies).Where(f => f.Id == id).FirstAsync();
		}

		public async Task<IEnumerable<Character>> GetCharactersInFranchiseAsync(int franchiseId)
		{
			List<Movie> movieList = (List<Movie>)await GetMoviesInFranchiseAsync(franchiseId);

			List<Character> characterList = new();
			foreach (Movie movie in movieList)
			{
				Movie currentMovie = await _movieService.GetById(movie.Id);
				foreach (Character character in currentMovie.Characters)
				{
					characterList.Add(character);
				}
			}
			return characterList;
		}

		public async Task<IEnumerable<Movie>> GetMoviesInFranchiseAsync(int franchiseId)
		{
			Franchise franchise = await GetById(franchiseId);
			return franchise.Movies.ToList();
		}

		public async Task Update(Franchise domainFranchise)
		{
			_context.Entry(domainFranchise).State = EntityState.Modified;

			await _context.SaveChangesAsync();
		}

		public async Task UpdateFranchiseMoviesAsync(int franchiseId, List<int> movies)
		{

			Franchise franchiseToUpdateMovies = await GetById(franchiseId);

			List<Movie> movieList = new();

			foreach (int movieId in movies)
			{
				Movie movie = await _movieService.GetById(movieId);
				if (movie == null)
				{
					throw new KeyNotFoundException();
				}
				movieList.Add(movie);
			}
			franchiseToUpdateMovies.Movies = movieList;
			await _context.SaveChangesAsync();

		}
	}
}
