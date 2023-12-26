namespace Universitas.Contracts.Repositories
{
    public interface IRepository <T>
    {
        public Task CreateAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(int id);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(int id);
    }
}
