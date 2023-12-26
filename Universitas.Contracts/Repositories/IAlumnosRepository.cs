using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IAlumnosRepository : IRepository<Alumno>
    {
        public Task<Alumno?> GetByIdAsync(int id);

        public Task<List<Alumno>> GetByApellidoAsync(string apellido);

        public Task<List<Alumno>> GetByMateriaAsync(Materia materia);
    }
}
