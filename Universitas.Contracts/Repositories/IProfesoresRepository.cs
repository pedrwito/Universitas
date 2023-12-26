using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IProfesoresRepository : IRepository<Profesor>
    {
        public Task<List<Profesor>> GetByApellidoAsync(string apellido);

        Task<bool> ExistsByNationalIdAsync(string national_id);
    }
}
