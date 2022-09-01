using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models.Domain;
using MovieCharacterAPI.Models.DTOs.Character;
using MovieCharacterAPI.Models.DTOs.Movie;
using MovieCharacterAPI.Services;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	[Consumes(MediaTypeNames.Application.Json)]
	public class MoviesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMovieService _movieService;

		public MoviesController(IMapper mapper, IMovieService movieService)
		{
			_mapper = mapper;
			_movieService = movieService;
		}

		/// <summary>
		/// Returns a list of all movies
		/// </summary>
		/// <returns>A list of MovieReadDTO</returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
		{
			return _mapper.Map<List<MovieReadDTO>>(await _movieService.GetAll());
		}

		/// <summary>
		/// Returns a specific movie
		/// </summary>
		/// <param name="id"></param>
		/// <returns>MovieReadDTO or Not Found if Movie is null</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
		{
			Movie movie = await _movieService.GetById(id);

			if (movie == null)
			{
				return NotFound();
			}

			return _mapper.Map<MovieReadDTO>(movie);
		}

		/// <summary>
		/// Returns all characters in a movie
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Not Found if movieId does not exist in DB, A list of CharacterReadDTO</returns>
		[HttpGet("{id}/characters")]
		public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInMovie(int id)
		{
			if (!_movieService.Exists(id))
			{
				return NotFound();
			}

			List<CharacterReadDTO> characterList = _mapper.Map<List<CharacterReadDTO>>(await _movieService.GetCharactersInMovieAsync(id));

			return Ok(characterList);
		}

		/// <summary>
		/// Updates a movie by ID and movie object
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dtoMovie"></param>
		/// <returns>No Content, Bad Request if movieId and Id does not match or Not Found if movieId does not exist in DB</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> PutMovie(int id, MovieEditDTO dtoMovie)
		{
			if (id != dtoMovie.Id)
			{
				return BadRequest();
			}

			if (!_movieService.Exists(id))
			{
				return NotFound();
			}

			Movie domainMovie = _mapper.Map<Movie>(dtoMovie);
			await _movieService.Update(domainMovie);

			return NoContent();
		}

		/// <summary>
		/// Upates all characters in a movie
		/// </summary>
		/// <param name="id"></param>
		/// <param name="characters"></param>
		/// <returns>Not Found if movie does not exist i DB, Bad Request or No Content</returns>
		[HttpPut("{id}/characters")]
		public async Task<IActionResult> UpdateMovieCharacters(int id, List<int> characters)
		{
			if (!_movieService.Exists(id))
			{
				return NotFound();
			}

			try
			{
				await _movieService.UpdateMovieCharacters(id, characters);
			}
			catch (KeyNotFoundException)
			{
				return BadRequest();
			}

			return NoContent();
		}


		/// <summary>
		/// Adds a movie to DB
		/// </summary>
		/// <param name="dtoMovie"></param>
		/// <returns>The created movie</returns>
		[HttpPost]
		public async Task<ActionResult<Movie>> PostMovie(MovieCreateDTO dtoMovie)
		{
			if (dtoMovie.FranchiseId == 0)
			{
				dtoMovie.FranchiseId = null;
			}
			Movie domainMovie = _mapper.Map<Movie>(dtoMovie);

			domainMovie = await _movieService.Add(domainMovie);

			return CreatedAtAction("GetMovie", new { id = domainMovie.Id }, _mapper.Map<MovieReadDTO>(domainMovie));
		}

		/// <summary>
		/// Deletes a movie by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns>No Content or Not Found if movieId does not exist in DB</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMovie(int id)
		{
			if (!_movieService.Exists(id))
			{
				return NotFound();
			}

			await _movieService.Delete(id);

			return NoContent();

		}
	}
}
