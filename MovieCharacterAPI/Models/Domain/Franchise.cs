using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.Domain
{
	public class Franchise
	{
		public int Id { get; set; }
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }
		[MaxLength(100)]
		public string Description { get; set; }
		public ICollection<Movie> Movies { get; set; }

	}
}
