using AutoMapper;
using MovieCharacterAPI.Models.Domain;
using MovieCharacterAPI.Models.DTOs.Movie;
using System.Linq;

namespace MovieCharacterAPI.Profiles
{
	public class MovieProfile : Profile
	{

		public MovieProfile()
		{
			CreateMap<Movie, MovieReadDTO>()
			.ForMember(pdto => pdto.Characters, opt =>
			opt.MapFrom(p => p.Characters.Select(s => s.Id).ToArray()))
				.ReverseMap();
			CreateMap<Movie, MovieCreateDTO>()
				.ReverseMap();
			CreateMap<Movie, MovieEditDTO>()
				.ReverseMap();
		}

	}
}
