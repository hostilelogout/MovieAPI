using MovieApi.Models.Domain;

namespace MovieApi.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        Task<ICollection<Movie>> GetMoviesAsync(int franchiseId);
        Task UpdateMoviesAsync(int[] movieIds, int franchiseId);
    }
}
