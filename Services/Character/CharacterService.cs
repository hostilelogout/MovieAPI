using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Services.Character
{
    public class CharacterService : ICharacterService
    {
        protected readonly MovieDbContext ? _context;

        public CharacterService(MovieDbContext ? context) => _context = context;

        public async Task<Models.Domain.Character> AddCharacterAsync(Models.Domain.Character character)
        {
            _context!.Character.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public bool CharacterExists(int id) => _context!.Character.Any(x => x.Id == id);

        public Task DeleteCharacterAsync(Models.Domain.Character character)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.Domain.Character>> GetAllCharacters()
        {
            return await _context!.Character
                .Include(c => c.AppearInMovies!)
                .ToListAsync();
        }

        public async Task<Models.Domain.Character> GetSpecificCharacterAsync(int id) => await _context!.Character.FindAsync(id);

        public Task UpdateCharacterAsync(Models.Domain.Character character)
        {
            throw new NotImplementedException();
        }
    }
}
