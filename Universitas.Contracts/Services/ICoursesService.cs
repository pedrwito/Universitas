using Universitas.Contracts.Models;

namespace Universitas.Contracts.Services
{
    public interface ICoursesService
    {
        Task<Course> CreateAsync(string name);

        Task<Course> UpdateAsync(int id, string name);

        Task DeleteAsync(int id);

        Task<Course> GetByIdAsync(int id);

        Task<IEnumerable<Course>> GetAllAsync();
    }
}
