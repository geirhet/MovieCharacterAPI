using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Data;
using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Services
{
	public class CharacterService : ICharacterService
	{
		private readonly MovieDbContext _context;

		public CharacterService(MovieDbContext context)
		{
			_context = context;
		}

		public async Task<Character> Add(Character character)
		{
			_context.Characters.Add(character);
			await _context.SaveChangesAsync();
			return character;
		}

		public bool Exists(int id)
		{
			return _context.Characters.Any(e => e.Id == id);
		}

		public async Task Delete(int id)
		{
			Character character = await _context.Characters.FindAsync(id);
			_context.Characters.Remove(character);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Character>> GetAll()
		{
			return await _context.Characters
				.Include(c => c.Movies)
				.ToListAsync<Character>();
		}

		public async Task<Character> GetById(int id)
		{
			if (!Exists(id)) return null;
			return await _context.Characters.Include(c => c.Movies).Where(c => c.Id == id).FirstAsync();
		}

		public async Task Update(Character character)
		{
			_context.Entry(character).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
