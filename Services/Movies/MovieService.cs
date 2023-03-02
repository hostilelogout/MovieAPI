using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Models.Domain;

namespace MovieApi.Services.Movies
{
    public class MovieService : IMovieService
    {
        protected readonly MovieDbContext _context;
        protected readonly ILogger<MovieService> _logger;

        public MovieService(MovieDbContext context, ILogger<MovieService> logger)
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
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                _logger.LogError("Movie not found with Id: " + id);
                throw new Exception();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
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
            if (!await MovieExistsAsync(id))
            {
                _logger!.LogError("Character not found with Id: " + id);
                throw new Exception();
            }

            return await _context!.Movie
                .Where(p => p.Id == id)
                .Include(p => p.Characters)
                .Include(p => p.Franchise)
                .FirstAsync();
        }

        public async Task UpdateAsync(Movie entity)
        {
            if (!await MovieExistsAsync(entity.Id))
            {
                _logger.LogError("Movie not found with Id: " + entity.Id);
                throw new Exception();
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> MovieExistsAsync(int id)
        {
            return await _context!.Movie.AnyAsync(x => x.Id == id);
        }

        public async Task<ICollection<Character>> GetCharactersAsync(int movieId)
        {
            if (!await MovieExistsAsync(movieId))
            {
                _logger.LogError("Movie not found with Id: " + movieId);
                throw new Exception();
            }

            return await _context.Movie
                .Where(m => m.Id == movieId)
                .SelectMany(m => m.Characters)
                .ToListAsync();
        }

        public async Task UpdateCharactersAsync(int[] characterIds, int movieId)
        {
            if (!await MovieExistsAsync(movieId))
            {
                _logger.LogError("Movie not found with Id: " + movieId);
                throw new Exception();
            }

            List<Character> characters = characterIds
                .ToList()
                .Select(cid => _context.Character
                .Where(c => c.Id == cid).First())
                .ToList();
            
            Movie movie = await _context.Movie
                .Where(m => m.Id == movieId)
                .FirstAsync();
            
            movie.Characters = characters;
            _context.Entry(movie).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
        }
    }
}
