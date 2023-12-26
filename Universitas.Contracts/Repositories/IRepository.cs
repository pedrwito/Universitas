namespace Universitas.Contracts.Repositories
{
    public interface IRepository <T>
    {
        public Task CreateAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
