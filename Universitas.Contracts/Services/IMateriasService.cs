using Universitas.Contracts.Models;

namespace Universitas.Contracts.Services
{
    public interface IMateriasService
    {
        Task<Materia> CreateAsync(string name);

        Task<Materia> UpdateAsync(int id, string name);

        Task DeleteAsync(int id);

        Task<Materia> GetByIdAsync(int id);

        Task<IEnumerable<Materia>> GetAllAsync();
    }
}
