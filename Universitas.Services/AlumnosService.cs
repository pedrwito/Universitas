using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Persistance;

namespace Universitas.Services
{
    public class AlumnosService : IAlumnosService
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
            
            if (checkIdRepeted && await Database.GetInstance().Alumnos.ExistsByNationalIdAsync(nationalId))
            {
                throw new ArgumentException("National Id already exists");
            }
        }

        public async Task<Alumno> CreateAsync(string name, string surname, string nationalId)
        {
            await ValidateStudentAsync(name, surname, nationalId);
            Alumno alumno = new Alumno(name, surname, nationalId); 
            await Database.GetInstance().Alumnos.CreateAsync(alumno);

            return alumno;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Alumnos.DeleteAsync(id);
            }
            catch (InvalidOperationException) 
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<IEnumerable<Alumno>> GetAllAsync()
        {
           return await Database.GetInstance().Alumnos.GetAllAsync();
        }

        public async Task<Alumno> GetByIdAsync(int id)
        {
           return await Database.GetInstance().Alumnos.GetByIdAsync(id) 
                ?? throw new KeyNotFoundException("The Id does not correspond to any student");
        }

        public async Task<Alumno> UpdateAsync(int id, string name, string surname, string nationalId)
        {
            Alumno student = await GetByIdAsync(id);
            
            await ValidateStudentAsync(name, surname, nationalId, student.NationalId != nationalId);

            student.NationalId = nationalId;
            student.Apellido = surname;
            student.Nombre = name;

            await Database.GetInstance().Alumnos.UpdateAsync(student);

            return student;
        }
    }
}
