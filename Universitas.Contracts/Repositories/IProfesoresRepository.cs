using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IProfesoresRepository : IRepository<Profesor>
    {
        public Task<Profesor?> GetByIdAsync(int id);

        public Task<List<Profesor>> GetByApellidoAsync(string apellido);
    }
}
