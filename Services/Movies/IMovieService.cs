using MovieApi.Models.Domain;

namespace MovieApi.Services.Movies
{
    public interface IMovieService : ICrudService<Movie,int>
    {
        Task<ICollection<Character>> GetCharactersAsync(int movieId);
        Task UpdateCharactersAsync(int[] characterIds, int movieId);
        protected Task<bool> MovieExistsAsync(int id);
    }
}
