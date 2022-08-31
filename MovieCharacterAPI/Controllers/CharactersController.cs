using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCharacterAPI.Models.Domain;
using MovieCharacterAPI.Models.DTOs.Character;
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

	public class CharactersController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ICharacterService _characterService;

		public CharactersController(IMapper mapper, ICharacterService characterService)
		{
			_mapper = mapper;
			_characterService = characterService;
		}

		/// <summary>
		/// Returns a list of all characters
		/// </summary>
		/// <returns>A list of characterReadDTOs</returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters()
		{
			return _mapper.Map<List<CharacterReadDTO>>(await _characterService.GetAll());
		}

		/// <summary>
		/// Returns a specific character
		/// </summary>
		/// <param name="id"></param>
		/// <returns>CharacterReadDTO or Not Found if Characte is null</returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet("{id}")]
		public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
		{
			Character character = await _characterService.GetById(id);

			if (character == null)
			{
				return NotFound();
			}

			return _mapper.Map<CharacterReadDTO>(character);
		}

		/// <summary>
		/// Updates a character by ID and Character object
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dtoCharacter"></param>
		/// <returns>No Content, Bad Request if characterId and Id does not match or Not Found if characterId does not exist in DB</returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCharacter(int id, CharacterEditDTO dtoCharacter)
		{
			if (id != dtoCharacter.Id)
			{
				return BadRequest();
			}

			if (!_characterService.Exists(id))
			{
				return NotFound();
			}

			Character domainCharacter = _mapper.Map<Character>(dtoCharacter);
			await _characterService.Update(domainCharacter);

			return NoContent();

		}

		/// <summary>
		/// Adds a character to DB
		/// </summary>
		/// <param name="dtoCharacter"></param>
		/// <returns>The created character</returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO dtoCharacter)
		{
			Character domainCharacter = _mapper.Map<Character>(dtoCharacter);

			domainCharacter = await _characterService.Add(domainCharacter);

			return CreatedAtAction("GetCharacter", new { id = domainCharacter.Id }, _mapper.Map<CharacterReadDTO>(domainCharacter));
		}

		/// <summary>
		/// Deletes a character by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns>No Content or Not Found if characterId does not exist in DB</returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCharacter(int id)
		{
			if (!_characterService.Exists(id))
			{
				return NotFound();
			}

			await _characterService.Delete(id);

			return NoContent();

		}
	}
}
