using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Domain
{
    [Table("Franchise")]
    public class Franchise
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Description { get; set; }
    }
}
