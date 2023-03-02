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
            CreateMap<CharacterEditDTO, Character>();
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(dto => dto.Movies, opt => opt
                .MapFrom(p => p.Movies!.Select(s => s.Id).ToList()));
        }
    }
}
