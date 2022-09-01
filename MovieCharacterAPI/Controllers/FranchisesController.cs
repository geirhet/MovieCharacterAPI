using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models.Domain;
using MovieCharacterAPI.Models.DTOs.Character;
using MovieCharacterAPI.Models.DTOs.Franchise;
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
	public class FranchisesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IFranchiseService _franchiseService;


		public FranchisesController(IMapper mapper, IFranchiseService franchiseService)
		{
			_mapper = mapper;
			_franchiseService = franchiseService;
		}

		/// <summary>
		/// Returns a list of all franchies
		/// </summary>
		/// <returns>A list of franchiseReadDTO</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetFranchises()
		{
			List<FranchiseReadDTO> franchiselist = _mapper.Map<List<FranchiseReadDTO>>(await _franchiseService.GetAll());
			return Ok(franchiselist);
		}

		/// <summary>
		/// Returns a specific franchise
		/// </summary>
		/// <param name="id"></param>
		/// <returns>FranchiseReadDTO or Not Found if Franchise is null</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
		{
			Franchise franchise = await _franchiseService.GetById(id);

			if (!_franchiseService.Exists(id))
			{
				return NotFound();
			}

			return _mapper.Map<FranchiseReadDTO>(franchise);
		}

		/// <summary>
		/// Returns all movie in a franchise
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Not Found if franchiseId does not exist in DB, A list of MovieReadDTOs</returns>
		[HttpGet("{id}/movies")]
		public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMoviesInFranchise(int id)
		{
			if (!_franchiseService.Exists(id))
			{
				return NotFound();
			}

			List<MovieReadDTO> movieList = _mapper.Map<List<MovieReadDTO>>(await _franchiseService.GetMoviesInFranchiseAsync(id));

			return Ok(movieList);
		}

		/// <summary>
		/// Returns all characters in a franchise
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Not Found if franchiseId does not exist in DB, A list of CharacterReadDTOs</returns>
		[HttpGet("{id}/characters")]
		public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInFranchise(int id)
		{
			if (!_franchiseService.Exists(id))
			{
				return NotFound();
			}

			List<CharacterReadDTO> characterList = _mapper.Map<List<CharacterReadDTO>>(await _franchiseService.GetCharactersInFranchiseAsync(id));

			return Ok(characterList);
		}

		/// <summary>
		/// Adds a franchise to DB
		/// </summary>
		/// <param name="dtoFranchise"></param>
		/// <returns>The created franchise</returns>

		[HttpPost]
		public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO dtoFranchise)
		{

			Franchise domainFranchise = _mapper.Map<Franchise>(dtoFranchise);

			domainFranchise = await _franchiseService.Add(domainFranchise);

			return CreatedAtAction("GetFranchise", new { id = domainFranchise.Id }, _mapper.Map<FranchiseReadDTO>(domainFranchise));
		}

		/// <summary>
		/// Updates a franchise by ID and franchise object
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dtoFranchise"></param>
		/// <returns>No Content, Bad Request if franchiseId and Id does not match or Not Found if franchiseId does not exist in DB</returns>

		[HttpPut("{id}")]
		public async Task<IActionResult> PutFranchise(int id, FranchiseEditDTO dtoFranchise)
		{
			if (id != dtoFranchise.Id)
			{
				return BadRequest();
			}
			if (!_franchiseService.Exists(id))
			{
				return NotFound();
			}

			Franchise domainFranchise = _mapper.Map<Franchise>(dtoFranchise);

			await _franchiseService.Update(domainFranchise);

			return NoContent();
		}

		/// <summary>
		/// Upates all movies in a franchise
		/// </summary>
		/// <param name="id"></param>
		/// <param name="movies"></param>
		/// <returns>Not Found if Franchise does not exist i DB, Bad Request or No Content</returns>
		[HttpPut("{id}/movies")]
		public async Task<IActionResult> UpdateFranchiseMovies(int id, List<int> movies)
		{
			if (!_franchiseService.Exists(id))
			{
				return NotFound();
			}

			try
			{
				await _franchiseService.UpdateFranchiseMoviesAsync(id, movies);
			}
			catch (KeyNotFoundException)
			{
				return BadRequest();
			}

			return NoContent();
		}

		/// <summary>
		/// Deletes a franchise by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns>No Content or Not Found if franchiseId does not exist in DB</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFranchise(int id)
		{
			if (!_franchiseService.Exists(id))
			{
				return NotFound();
			}

			await _franchiseService.Delete(id);

			return NoContent();

		}
	}
}
