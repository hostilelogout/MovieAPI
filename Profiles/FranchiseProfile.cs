using AutoMapper;
using MovieApi.Models.Domain;
using MovieApi.Models.DTO.Franchises;

namespace MovieApi.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<FranchiseCreateDTO, Franchise>();
            CreateMap<FranchiseDeleteDTO, Franchise>();
            CreateMap<FranchiseEditDTO, Franchise>();
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(dto => dto.Movies, opt => opt
                .MapFrom(p => p.Movies.Select(s => s.Id).ToList()));
        }
    }
}
