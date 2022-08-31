using AutoMapper;
using MovieCharacterAPI.Models.Domain;
using MovieCharacterAPI.Models.DTOs.Franchise;
using System.Linq;

namespace MovieCharacterAPI.Profiles
{
	public class FranchiseProfile : Profile
	{

		public FranchiseProfile()
		{
			CreateMap<Franchise, FranchiseReadDTO>()
				.ForMember(pdto => pdto.Movies, opt =>
				 opt.MapFrom(p => p.Movies.Select(s => s.Id).ToArray()))
				.ReverseMap();
			CreateMap<Franchise, FranchiseCreateDTO>()
				.ReverseMap();
			CreateMap<Franchise, FranchiseEditDTO>()
				.ReverseMap();
		}

	}
}
