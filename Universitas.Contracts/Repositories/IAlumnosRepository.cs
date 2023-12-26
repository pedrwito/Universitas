using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IAlumnosRepository : IRepository<Alumno>
    {
        public Task<List<Alumno>> GetByApellidoAsync(string apellido);

        public Task<List<Alumno>> GetByMateriaAsync(Materia materia);

        Task<bool> ExistsByNationalIdAsync(string national_id);
    }
}
