using MovieApi.Models.Domain;

namespace MovieApi.Services.Movies
{
    public interface IMovieService : ICrudService<Movie,int>
    {
        protected Task<bool> MovieExistsAsync(int id);
    }
}
