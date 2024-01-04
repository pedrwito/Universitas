using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IProfessorsRepository : IRepository<Professor>
    {
        public Task<List<Professor>> GetBySurnameAsync(string surname);

        Task<bool> ExistsByNationalIdAsync(string nationalId);

        Task<bool> ExistsByIdAsync(int id);

        Task<List<Professor>> GetByCourseAsync(int courseID);
    }
}
