using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IStudentsRepository : IRepository<Student>
    {
        Task<List<Student>> GetBySurnameAsync(string surname);

        Task<List<Student>> GetByCourseAsync(int courseId);

        Task<bool> ExistsByNationalIdAsync(string national_id);

        Task<bool> ExistsByIdAsync(int id);

        Task<int> GetAmountOfCorsesEnrolled(Student student);

    }
}
