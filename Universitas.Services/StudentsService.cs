using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Persistance;

namespace Universitas.Services
{
    public class StudentsService : IStudentsService
    {
        private async Task ValidateStudentAsync(string name, string surname, string nationalId, bool checkIdRepeted = true, int? status = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentNullException("Surname cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(nationalId))
            {
                throw new ArgumentNullException("National Id cannot be empty");
            }
            
            if (checkIdRepeted && await Database.GetInstance().Students.ExistsByNationalIdAsync(nationalId))
            {
                throw new ArgumentException("National Id already exists");
            }

            if (status != null && !Enum.IsDefined(typeof(StudentStatus), status)) 
            {
                throw new ArgumentException("Status value is invalid");
            }
        }

        public async Task<Student> CreateAsync(string name, string surname, string nationalId)
        {
            await ValidateStudentAsync(name, surname, nationalId);
            Student alumno = new Student(name, surname, nationalId); 
            await Database.GetInstance().Students.CreateAsync(alumno);

            return alumno;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Students.DeleteAsync(id);
            }
            catch (InvalidOperationException) 
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
           return await Database.GetInstance().Students.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
           return await Database.GetInstance().Students.GetByIdAsync(id) 
                ?? throw new KeyNotFoundException("The Id does not correspond to any student");
        }

        public async Task<Student> UpdateAsync(int id, string name, string surname, string nationalId, int status)
        {
            Student student = await GetByIdAsync(id);
            
            await ValidateStudentAsync(name, surname, nationalId, student.NationalId != nationalId, status);

            student.NationalId = nationalId;
            student.Surname = surname;
            student.Name = name;
            student.Status = (StudentStatus)status;

            await Database.GetInstance().Students.UpdateAsync(student);

            return student;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(int id)
        {
            if (!await Database.GetInstance().Courses.ExistsByIdAsync(id))
            {
                throw new KeyNotFoundException("The Id does not correspond to any course");
            }
                 
            IEnumerable<Course> courseList = await Database.GetInstance().Courses.GetByStudentAsync(id);

            return courseList;
        }
    }
}
