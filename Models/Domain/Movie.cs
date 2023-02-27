using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        public int Id { get; set; }
        public string ? MovieTitle { get; set; }
        public enum Genres { }
        public Genres ? Genre { get; set; }

        public DateTime ? ReleaseYear { get; set; }

        public string ? Director { get; set; }

        public string ? PictureUrl { get; set; }

        public string ? TrailerUrl { get; set; }
    }
}
