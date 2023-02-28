using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        // PK
        public int Id { get; set; }
        // Fields
        [MaxLength(50)]
        public string ? MovieTitle { get; set; }
        [MaxLength(50)]
        public string ? Genre { get; set; }
        public DateTime ? ReleaseYear { get; set; }
        [MaxLength(50)]
        public string ? Director { get; set; }
        [MaxLength(100)]
        public string ? PictureUrl { get; set; }
        [MaxLength(100)]
        public string ? TrailerUrl { get; set; }
        // Relationships
        public Franchise ? Franchise { get; set; }
        public ICollection<Character> ? Characters { get; set; }
    }
}
