using AutoMapper;
using MovieCharacterAPI.Models.Domain;
using MovieCharacterAPI.Models.DTOs.Character;
using System.Linq;

namespace MovieCharacterAPI.Profiles
{
	public class CharacterProfile : Profile
	{

		public CharacterProfile()
		{
			CreateMap<Character, CharacterReadDTO>()
				.ForMember(pdto => pdto.Movies, opt =>
				 opt.MapFrom(p => p.Movies.Select(s => s.Id).ToArray()))
				.ReverseMap();
			CreateMap<Character, CharacterCreateDTO>()
				.ReverseMap();
			CreateMap<Character, CharacterEditDTO>()
				.ReverseMap();
		}
	}
}
