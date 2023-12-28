using Universitas.Contracts.Models;

namespace Universitas.Contracts.Services
{
    public interface IProfessorsService
    {
        Task<Professor> CreateAsync(string name, string surname, string nationalId);

        Task<Professor> UpdateAsync(int id, string name, string surname, string nationalId);

        Task DeleteAsync(int id);

        Task<Professor> GetByIdAsync(int id);

        Task<IEnumerable<Professor>> GetAllAsync();
    }
}
