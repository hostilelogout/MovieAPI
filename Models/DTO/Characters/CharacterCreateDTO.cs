using MovieApi.Models.Domain;

namespace MovieApi.Models.DTO.Characters
{
    public class CharacterCreateDTO
    {
        public string? FullName { get; set; } = null!;
        public string? Alias { get; set; } = null!;
        public string? Gender { get; set; } = null!;
        public string? PictureUrl { get; set; } = null!;
    }
}
