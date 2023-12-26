using Universitas.Contracts.Models;

namespace Universitas.Contracts.Services
{
    public interface IProfesoresService
    {
        Task<Profesor> CreateAsync(string name, string surname, string nationalId);

        Task<Profesor> UpdateAsync(int id, string name, string surname, string nationalId);

        Task DeleteAsync(int id);

        Task<Profesor> GetByIdAsync(int id);

        Task<IEnumerable<Profesor>> GetAllAsync();
    }
}
