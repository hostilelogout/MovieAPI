using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Models.Domain;

namespace MovieApi.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieDbContext _movieDbContext;
        private readonly ILogger<FranchiseService> _logger;

        public FranchiseService(MovieDbContext movieDbContext, ILogger<FranchiseService> logger)
        {
            _movieDbContext = movieDbContext;
            _logger = logger;
        }

        public async Task AddAsync(Franchise entity)
        {
            await _movieDbContext.AddAsync(entity);
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var franchise = await _movieDbContext.Franchise.FindAsync(id);
            // Log and throw pattern
            if (franchise == null)
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new Exception();
            }
            // We set our entities to have nullable relationships
            // so it removes the FKs when delete it.
            _movieDbContext.Franchise.Remove(franchise);
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _movieDbContext.Franchise
                .Include(p => p.Movies)
                .ToListAsync();

        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!await FranchiseExistsAsync(id))
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new Exception();
            }
            // Want to include all related data for franchise
            return await _movieDbContext.Franchise
                .Where(p => p.Id == id)
                .Include(p => p.Movies)
                .FirstAsync();
        }

        public async Task<ICollection<Character>> GetCharactersAsync(int franchiseId)
        {
            // Log and throw error handling
            if (!await FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new Exception();
            }
            // Dont need to include related data because of the DTO we are mapping to.
            // This can change depending on the business requirements.
            return await _movieDbContext.Movie
                .Where(m => m.FranchiseId == franchiseId)
                .SelectMany(m => m.Characters).Distinct()
                .Include(p => p.Movies)
                .ToListAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesAsync(int franchiseId)
        {
            // Log and throw error handling
            if (!await FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new Exception();
            }
            // Dont need to include related data because of the DTO we are mapping to.
            // This can change depending on the business requirements.
            return await _movieDbContext.Movie
                .Where(m => m.FranchiseId == franchiseId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Franchise entity)
        {
            // Log and throw pattern
            if (!await FranchiseExistsAsync(entity.Id))
            {
                _logger.LogError("Franchise not found with Id: " + entity.Id);
                throw new Exception();
            }
            _movieDbContext.Entry(entity).State = EntityState.Modified;
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task UpdateMoviesAsync(int[] movieIds, int franchiseId)
        {
            // Log and throw pattern
            if (!await FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new Exception();
            }
            // First convert the int[] to List<Student>

            // Could utilize the StudentNotFoundException here
            // if there is an Id in the array that doesnt have a student.
            // That would require a different interaction with EF to check for exception.
            // It is not done in this implementation.
            List<Movie> movies = movieIds
                .ToList()
                .Select(sid => _movieDbContext.Movie
                .Where(s => s.Id == sid).First())
                .ToList();
            // Get professor for Id
            Franchise franchise = await _movieDbContext.Franchise
                .Where(p => p.Id == franchiseId)
                .FirstAsync();
            // Set the professors students
            franchise.Movies = movies;
            _movieDbContext.Entry(franchise).State = EntityState.Modified;
            // Save all the changes
            await _movieDbContext.SaveChangesAsync();
        }

        private async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _movieDbContext.Franchise.AnyAsync(e => e.Id == id);
        }
    }
}
