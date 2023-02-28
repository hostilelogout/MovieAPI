using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Models.Domain;

namespace MovieApi.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        protected readonly MovieDbContext ? _context;

        public CharacterService(MovieDbContext ? context) => _context = context;

        public async Task<Character> AddCharacterAsync(Character character)
        {
            _context!.Character.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public bool CharacterExists(int id) => _context!.Character.Any(x => x.Id == id);

        public async Task DeleteCharacterAsync(int id)
        {
            Character ?character = await _context!.Character.FindAsync(id);
            _context!.Character.Remove(character!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _context!.Character
                .Include(c => c.Movies!)
                .ToListAsync();
        }

        public async Task<Character> GetSpecificCharacterAsync(int id) => await _context!.Character.FindAsync(id);

        public async Task UpdateCharacterAsync(Character character)
        {
            _context!.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
