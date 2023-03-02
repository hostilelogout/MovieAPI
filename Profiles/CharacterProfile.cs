using AutoMapper;
using MovieApi.Models.Domain;
using MovieApi.Models.DTO.Characters;

namespace MovieApi.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<CharacterCreateDTO, Character>();
            CreateMap<CharacterDeleteDTO, Character>();
            CreateMap<Character, CharacterEditDTO>();
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(x => x.Movies, y => y
                .MapFrom(p => p.Movies!
                .ToDictionary(x => x.Id, x => x.MovieTitle)))
                .ReverseMap();

        }
    }
}
