using MovieCharacterAPI.Models.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models.Domain
{
	public class Movie
	{
		public int Id { get; set; }
		[MaxLength(100)]
		[Required]
		public string Title { get; set; }
		[MaxLength(100)]
		public string Genre { get; set; }
		public int? ReleaseYear { get; set; }
		[MaxLength(100)]
		public string Director { get; set; }
		[MaxLength(100)]
		public string Picture { get; set; }
		[MaxLength(100)]
		public string Trailer { get; set; }
		public int? FranchiseId { get; set; }
		public Franchise Franchise { get; set; }
		public ICollection<Character> Characters { get; set; }
	}
}
