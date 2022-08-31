using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.Domain
{
	public class Character
	{
		public int Id { get; set; }
		[MaxLength(100)]
		[Required]
		public string FullName { get; set; }
		[MaxLength(100)]
		public string Alias { get; set; }
		[MaxLength(20)]
		public string Gender { get; set; }
		[MaxLength(100)]
		public string Picture { get; set; }
		public ICollection<Movie> Movies { get; set; }
	}
}
