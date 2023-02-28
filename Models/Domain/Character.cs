using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Domain
{
    [Table("Character")]
    public class Character
    {
        // PK
        public int Id { get; set; }
        // Fields
        [MaxLength(50)]
        public string ? FullName { get; set; }
        [MaxLength(50)]
        public string ? Alias { get; set; }
        [MaxLength(50)]
        public string ? Gender { get; set; }
        [MaxLength(100)]
        public string ? PictureUrl { get; set; }
        // Relationships
        public ICollection<Movie> ? Movies { get; set; }

    }
}
