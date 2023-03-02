using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Models.Domain;

namespace MovieApi.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        protected readonly MovieDbContext ? _context;
        protected readonly ILogger<CharacterService> ? _logger;

        public CharacterService(MovieDbContext ? context, ILogger<CharacterService> ? logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Character entity)
        {
            await _context!.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            Character? character = await _context!.Character.FindAsync(id);

            if (character == null)
            {
                _logger!.LogError("Character not found with Id: " + id);
                throw new Exception();
            }
            _context!.Character.Remove(character!);
            await _context!.SaveChangesAsync();
        }

        public async Task<ICollection<Character>> GetAllAsync()
        {
            return await _context!.Character
                .Include(x => x.Movies)
                .ToListAsync();
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!await CharacterExistsAsync(id))
            {
                _logger!.LogError("Character not found with Id: " + id);
                throw new Exception();
            }
            // Want to include all related data for character
            return await _context!.Character
                .Where(p => p.Id == id)
                .Include(p => p.Movies)
                .FirstAsync();
        }

        public async Task UpdateAsync(Character entity)
        {
            // Log and throw pattern
            if (!await CharacterExistsAsync(entity.Id))
            {
                _logger!.LogError("Character not found with Id: " + entity.Id);
                throw new Exception();
            }
            _context!.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task AddCharacterToMovie(int movieId, int id)
        {
            ICollection<Movie> movie = await _context!.Movie
                .Where(x => x.Id == movieId) .ToListAsync();

            Character character = await _context!.Character
                .Where(p => p.Id == id)
                .FirstAsync();

            character.Movies = movie;

            if (await CharacterHasMovie(character.Id, movieId))
            {
                _logger!.LogError("Character already contains movie : " + character.Movies.First().MovieTitle);
                throw new Exception();
            }

            _context.Entry(character).State = EntityState.Modified;
            // Save all the changes
            await _context.SaveChangesAsync();
        }

        public async Task AddCharacterToMultipleMovies(int[] movieIds, int id)
        {
            List<Movie> movies = movieIds
                .ToList()
                .Select(sid => _context!.Movie
                .Where(s => s.Id == sid).First())
                .ToList();

            Character character = await _context!.Character
                .Where(p => p.Id == id)
                .FirstAsync();

            character.Movies = movies;


            _context.Entry(character).State = EntityState.Modified;
            // Save all the changes
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context!.Character.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CharacterHasMovie(int charId, int movieId)
        {
            return await _context!.Character.AnyAsync(x => x.Id == charId && x.Movies!.Any(x => x.Id == movieId));
        }
      
    }
}
