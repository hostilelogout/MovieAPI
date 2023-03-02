
using MovieApi.Models.Domain;

namespace MovieApi.Services.Characters
{
    public interface ICharacterService : ICrudService<Character,int>
    {
        protected Task<bool> CharacterExistsAsync(int id);
        protected Task<bool> CharacterHasMovie(int charId, int movieId);
        public Task AddCharacterToMovie(int movieId, int id);
        public Task AddCharacterToMultipleMovies(int[] movieIds, int id);
    }
}
