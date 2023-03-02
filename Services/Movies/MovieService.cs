using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Models.Domain;

namespace MovieApi.Services.Movies
{
    public class MovieService : IMovieService
    {
        protected readonly MovieDbContext? _context;
        protected readonly ILogger<MovieService>? _logger;

        public MovieService(MovieDbContext? context, ILogger<MovieService>? logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Movie entity)
        {
            await _context!.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _context!.Movie                 
                 .Include(x => x.Characters)
                 .Include(x => x.Franchise)
                 .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!await MovieExistsAsync(id))
            {
                _logger!.LogError("Character not found with Id: " + id);
                throw new Exception();
            }
            // Want to include all related data for movie
            return await _context!.Movie
                .Where(p => p.Id == id)
                .Include(p => p.Characters)
                .Include(p => p.Franchise)
                .FirstAsync();
        }

        public async Task UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MovieExistsAsync(int id)
        {
            return await _context!.Movie.AnyAsync(x => x.Id == id);
        }
    }
}
