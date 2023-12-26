using Universitas.Contracts.Models;

namespace Universitas.Contracts.Services
{
    public interface IAlumnosService
    {
        Task<Alumno> CreateAsync(string name, string surname, string nationalId);

        Task<Alumno> UpdateAsync(int id, string name, string surname, string nationalId);

        Task DeleteAsync(int id);

        Task<Alumno> GetByIdAsync(int id);

        Task<IEnumerable<Alumno>> GetAllAsync();

    }
}
