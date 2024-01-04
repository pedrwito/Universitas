using Universitas.Contracts.Models;

namespace Universitas.Contracts.Services
{
    public interface ICoursesService
    {
        Task<Course> CreateAsync(string name);

        Task<Course> UpdateAsync(int id, string name);

        Task DeleteAsync(int id);

        Task<Course> GetByIdAsync(int id);

        Task<IEnumerable<Course>> GetAllAsync();

        Task EnrollStudentAsync(int studentId, int courseId);

        Task EnrollProfessorAsync(int professorId, int courseId);

        Task RemoveProfessorAsync(int professorId, int courseId);

        Task RemoveStudentAsync(int studentId, int courseId);

        Task<IEnumerable<Student>> GetStudentsAsync(int id);

        Task<IEnumerable<Professor>> GetProfessorsAsync(int id);   
    }
}
