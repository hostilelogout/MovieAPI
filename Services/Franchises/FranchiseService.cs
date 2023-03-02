using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Models.Domain;

namespace MovieApi.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieDbContext _context;
        private readonly ILogger<FranchiseService> _logger;

        public FranchiseService(MovieDbContext context, ILogger<FranchiseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Franchise entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var franchise = await _context.Franchise.FindAsync(id);
            if (franchise == null)
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new Exception();
            }

            _context.Movie.Load();
            _context.Franchise.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _context.Franchise
                .Include(p => p.Movies)
                .ToListAsync();

        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            if (!await FranchiseExistsAsync(id))
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new Exception();
            }

            return await _context.Franchise
                .Where(p => p.Id == id)
                .Include(p => p.Movies)
                .FirstAsync();
        }

        public async Task<ICollection<Character>> GetCharactersAsync(int franchiseId)
        {
            if (!await FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new Exception();
            }

            return await _context.Movie
                .Where(m => m.FranchiseId == franchiseId)
                .SelectMany(m => m.Characters).Distinct()
                .Include(p => p.Movies)
                .ToListAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesAsync(int franchiseId)
        {
            if (!await FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new Exception();
            }

            return await _context.Movie
                .Where(m => m.FranchiseId == franchiseId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Franchise entity)
        {
            if (!await FranchiseExistsAsync(entity.Id))
            {
                _logger.LogError("Franchise not found with Id: " + entity.Id);
                throw new Exception();
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMoviesAsync(int[] movieIds, int franchiseId)
        {
            if (!await FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new Exception();
            }

            List<Movie> movies = movieIds
                .ToList()
                .Select(sid => _context.Movie
                .Where(s => s.Id == sid).First())
                .ToList();

            Franchise franchise = await _context.Franchise
                .Where(p => p.Id == franchiseId)
                .FirstAsync();

            franchise.Movies = movies;
            _context.Entry(franchise).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        private async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _context.Franchise.AnyAsync(e => e.Id == id);
        }
    }
}
