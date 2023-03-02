﻿using MovieApi.Models.Domain;
using System.ComponentModel.DataAnnotations;
namespace MovieApi.Models.DTO.Movies
{
    public class MovieCreateDTO
    {
        public int Id { get; set; }
        // Fields
        [MaxLength(50)]
        public string? MovieTitle { get; set; }
        [MaxLength(50)]
        public string? Genre { get; set; }
        public DateTime? ReleaseYear { get; set; }
        [MaxLength(50)]
        public string? Director { get; set; }
        [MaxLength(100)]
        public string? PictureUrl { get; set; }
        [MaxLength(100)]
        public string? TrailerUrl { get; set; }
        // Relationships
        public int? FranchiseId { get; set; }
        public Franchise? Franchise { get; set; }
        public ICollection<Character>? Characters { get; set; }
    }
}
