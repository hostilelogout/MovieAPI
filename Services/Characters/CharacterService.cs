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

        public Task UpdateAsync(Character entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context!.Character.AnyAsync(x => x.Id == id);
        }

    }
}
