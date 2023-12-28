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
    }
}
