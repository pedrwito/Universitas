using Universitas.Contracts.Models;

namespace Universitas.Contracts.Repositories;

public interface ICoursesRepository : IRepository<Course>
{
    public Task<List<Course>> GetByNameAsync(string name);

    Task<List<Course>> GetByStudentAsync(int studentID);

    Task<List<Course>> GetByProfessorAsync(int professorId);

    Task<bool> ExistsByIdAsync(int id);

    Task EnrollStudentAsync(int studentId, int courseId);

    Task EnrollProfessorAsync(int professorId, int courseId);

    Task RemoveProfessorAsync(int professorId, int courseId);

    Task RemoveStudentAsync(int studentId, int courseId);
}
