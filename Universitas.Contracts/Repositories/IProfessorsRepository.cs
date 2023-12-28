using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IProfessorsRepository : IRepository<Professor>
    {
        public Task<List<Professor>> GetBySurnameAsync(string surname);

        Task<bool> ExistsByNationalIdAsync(string national_id);
    }
}
