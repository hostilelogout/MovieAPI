using MovieApi.Models.Domain;

namespace MovieApi.Services.Movies
{
    public class MovieService : IMovieService
    {
        public Task AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MovieExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
