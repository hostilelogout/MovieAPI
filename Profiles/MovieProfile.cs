using AutoMapper;
using MovieApi.Models.Domain;
using MovieApi.Models.DTO.Characters;
using MovieApi.Models.DTO.Movies;

namespace MovieApi.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieDeleteDTO, Movie>();
            CreateMap<MovieEditDTO, Movie>();
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(x => x.Franchise, y => y
                .MapFrom(p => p.FranchiseId))
                //.MapFrom((x,y) => y.Franchise = new KeyValuePair<int, string>(x.Franchise!.Id,x.Franchise.Name!)))
                .ForMember(x => x.Characters, y => y
                .MapFrom(p => p.Characters!
                .ToDictionary(x => x.Id, x => x.FullName)));
        }
    }
}
