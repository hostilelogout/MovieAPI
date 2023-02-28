﻿using MovieApi.Models.Domain;

namespace MovieApi.Models.DTO.Character
{
    public class CharacterReadDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Alias { get; set; }
        public string? Gender { get; set; }
        public string? PictureUrl { get; set; }

        public ICollection<Domain.Movie>? AppearInMovies { get; set; }
    }
}
