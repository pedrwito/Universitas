using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Persistance;

namespace Universitas.Services
{
    public class CoursesService : ICoursesService
    {

        private void ValidateCourse(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be empty");
            }  
        }

        private async Task ExistsByIdOrThrowAsync(int id)
        {
            if (!await Database.GetInstance().Courses.ExistsByIdAsync(id))
            {
                throw new KeyNotFoundException("The Id does not correspond to any course");
            }
        }

        public async Task<Course> CreateAsync(string name)
        {
            ValidateCourse(name);
            Course Course = new Course(name);
            await Database.GetInstance().Courses.CreateAsync(Course);

            return Course;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Courses.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await Database.GetInstance().Courses.GetAllAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await Database.GetInstance().Courses.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException("The Id does not correspond to any course");
        }

        public async Task<Course> UpdateAsync(int id, string name)
        {
            ValidateCourse(name);

            Course course = await GetByIdAsync(id);

            course.Name = name;

            await Database.GetInstance().Courses.UpdateAsync(course);

            return course;
        }

        public async Task EnrollStudentAsync(int studentId, int courseId)
        {
            await ExistsByIdOrThrowAsync(courseId);

            if (!await Database.GetInstance().Students.ExistsByIdAsync(studentId))
            {
                throw new KeyNotFoundException("The Id does not correspond to any student");
            }

            await Database.GetInstance().Courses.EnrollStudentAsync(studentId, courseId);
        }

        public async Task EnrollProfessorAsync(int professorId, int courseId)
        {
            await ExistsByIdOrThrowAsync(courseId);

            if (!await Database.GetInstance().Professors.ExistsByIdAsync(professorId))
            {
                throw new KeyNotFoundException("The Id does not correspond to any professor");
            }

            await Database.GetInstance().Courses.EnrollStudentAsync(professorId, courseId);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(int courseId)
        {
            await ExistsByIdOrThrowAsync(courseId);

            return  await Database.GetInstance().Students.GetByCourseAsync(courseId);
        }

        public async Task<IEnumerable<Professor>> GetProfessorsAsync(int courseId)
        {
            return await Database.GetInstance().Professors.GetByCourseAsync(courseId);
        }

        public async Task RemoveProfessorAsync(int professorId, int courseId)
        {
            //TODO check if professor in course
            await Database.GetInstance().Courses.RemoveProfessorAsync(professorId,courseId);
        }

        public async Task RemoveStudentAsync(int studentId, int courseId)
        {
            //TODO check if student in course
            await Database.GetInstance().Courses.RemoveStudentAsync(studentId, courseId);
        }
    }
}
