using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories
{
    public interface IStudentsRepository : IRepository<Student>
    {
        Task<List<Student>> GetBySurnameAsync(string surname);

        Task<List<Student>> GetByCourseAsync(Course course);

        Task<bool> ExistsByNationalIdAsync(string national_id);

        Task<int> GetAmountOfCorsesEnrolled(Student alumno);

    }
}
