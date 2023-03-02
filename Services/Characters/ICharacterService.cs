
using MovieApi.Models.Domain;

namespace MovieApi.Services.Characters
{
    public interface ICharacterService : ICrudService<Character,int>
    {
        protected Task<bool> CharacterExistsAsync(int id);
    }
}
