using MovieApi.Models.Domain;

namespace MovieApi.Models.DTO.Character
{
    public class CharacterCreateDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Alias { get; set; }
        public string? Gender { get; set; }
        public string? PictureUrl { get; set; }

        public ICollection<Domain.Movie> ? Movies { get; set; }
    }
}
