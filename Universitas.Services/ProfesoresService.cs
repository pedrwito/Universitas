using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Persistance;

namespace Universitas.Services
{
    public class ProfesoresService : IProfesoresService
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

            if (checkIdRepeted && await Database.GetInstance().Profesores.ExistsByNationalIdAsync(nationalId))
            {
                throw new ArgumentException("National Id already exists");
            }
        }

        public async Task<Profesor> CreateAsync(string name, string surname, string nationalId)
        {
            await ValidateStudentAsync(name, surname, nationalId);
            Profesor profesor = new Profesor(name, surname, nationalId);
            await Database.GetInstance().Profesores.CreateAsync(profesor);

            return profesor;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Profesores.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            return await Database.GetInstance().Profesores.GetAllAsync();
        }

        public async Task<Profesor> GetByIdAsync(int id)
        {
            return await Database.GetInstance().Profesores.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException("The Id does not correspond to any professor");
        }

        public async Task<Profesor> UpdateAsync(int id, string name, string surname, string nationalId)
        {
            Profesor professor = await GetByIdAsync(id);

            await ValidateStudentAsync(name, surname, nationalId, professor.NationalId != nationalId);

            professor.NationalId = nationalId;
            professor.Apellido = surname;
            professor.Nombre = name;

            await Database.GetInstance().Profesores.UpdateAsync(professor);

            return professor;
        }

    }
}
