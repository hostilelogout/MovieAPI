
namespace MovieApi.Services.Character
{
    public interface ICharacterService
    {
        public Task<IEnumerable<Models.Domain.Character>> GetAllCharacters();
        public Task<Models.Domain.Character> GetSpecificCharacterAsync(int id);
        public Task<Models.Domain.Character> AddCharacterAsync(Models.Domain.Character character);
        public Task UpdateCharacterAsync(Models.Domain.Character character);
        public Task DeleteCharacterAsync(int id);
        public bool CharacterExists(int id);
    }
}
