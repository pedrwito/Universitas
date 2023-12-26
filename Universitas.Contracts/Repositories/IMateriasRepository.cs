using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories;

public interface IMateriasRepository : IRepository<Materia>
{
    public Task<Materia?> GetByIdAsync(int id);

    public Task<List<Materia>> GetByNombreAsync(string nombre);
}
