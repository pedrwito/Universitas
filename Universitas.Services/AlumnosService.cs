using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Persistance;

namespace Universitas.Services
{
    public class StudentsService : IStudentsService
    {
        private async Task ValidateStudentAsync(string name, string surname, string nationalId, bool checkIdRepeted = true)
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

        public async Task<Student> UpdateAsync(int id, string name, string surname, string nationalId)
        {
            Student student = await GetByIdAsync(id);
            
            await ValidateStudentAsync(name, surname, nationalId, student.NationalId != nationalId);

            student.NationalId = nationalId;
            student.Surname = surname;
            student.Name = name;

            await Database.GetInstance().Students.UpdateAsync(student);

            return student;
        }
    }
}
