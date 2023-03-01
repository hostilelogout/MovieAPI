namespace MovieApi.Services
{
    public interface ICrudService<T, Id>
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(Id id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(Id id);
    }
}
