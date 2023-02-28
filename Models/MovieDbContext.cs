using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Domain;

namespace MovieApi.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Character> Character { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
    }
}
