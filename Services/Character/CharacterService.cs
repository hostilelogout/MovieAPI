namespace MovieApi.Services.Character
{
    public class CharacterService : ICharacterService
    {
        public Task<Models.Domain.Character> AddCharacterAsync(Models.Domain.Character character)
        {
            throw new NotImplementedException();
        }

        public bool CharacterExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCharacterAsync(Models.Domain.Character character)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Domain.Character>> GetAllCharacters()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Domain.Character> GetSpecificCharacterAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharacterAsync(Models.Domain.Character character)
        {
            throw new NotImplementedException();
        }
    }
}
