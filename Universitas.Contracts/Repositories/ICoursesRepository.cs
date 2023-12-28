using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories;

public interface ICoursesRepository : IRepository<Course>
{
    public Task<List<Course>> GetByNameAsync(string name);
}
