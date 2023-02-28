using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Domain
{
    [Table("Franchise")]
    public class Franchise
    {
        // PK
        public int Id { get; set; }
        // Fields
        [MaxLength(50)]
        public string ? Name { get; set; }
        [MaxLength(200)]
        public string ? Description { get; set; }
        // Relationships
        public ICollection<Movie> ? Movies { get; set; }
    }
}
