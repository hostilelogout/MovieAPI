using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Domain;

namespace MovieApi.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Character> Character { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Franchise> Franchise { get; set; }

        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, FullName = "Bob Bobson", Alias = "Bobster", Gender = "Male" },
                new Character { Id = 2, FullName = "Jane Janeson", Alias = "JJ", Gender = "Female" },
                new Character { Id = 3, FullName = "Allison Allison", Alias = "Allison", Gender = "Female" }
                );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, MovieTitle = "Sad Story", Genre = "Action", ReleaseYear = new DateTime(2015, 5, 2), Director = "Person Peerson", FranchiseId = 1 },
                new Movie { Id = 2, MovieTitle = "Fun Story", Genre = "Horror", ReleaseYear = new DateTime(2010, 8, 3), Director = "Juste Somee McGuye", FranchiseId = 2 },
                new Movie { Id = 3, MovieTitle = "Bad Story", Genre = "Thriller", ReleaseYear = new DateTime(2020, 2, 27), Director = "Nho Bhody", FranchiseId = 1 }
                );

            modelBuilder.Entity<Franchise>().HasData(
                new Franchise { Id = 1, Name = "-ad Series", Description = "All additions to the -ad series of movies." },
                new Franchise { Id = 2, Name = "Paradox Collection", Description = "All the movies with a twist." }
                );

            modelBuilder.Entity<Movie>()
                .HasMany(mov => mov.Characters)
                .WithMany(cha => cha.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCharacter",
                    r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    je =>
                    {
                        je.HasKey("MovieId", "CharacterId");
                        je.HasData(
                            new { MovieId = 1, CharacterId = 1 },
                            new { MovieId = 1, CharacterId = 2 },
                            new { MovieId = 2, CharacterId = 2 },
                            new { MovieId = 2, CharacterId = 3 },
                            new { MovieId = 3, CharacterId = 1 },
                            new { MovieId = 3, CharacterId = 2 },
                            new { MovieId = 3, CharacterId = 3 }
                        );
                    });

        }

    }
}
