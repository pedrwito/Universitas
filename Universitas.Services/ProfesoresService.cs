using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Persistance;

namespace Universitas.Services
{
    public class ProfessorsService : IProfessorsService
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

            if (checkIdRepeted && await Database.GetInstance().Professors.ExistsByNationalIdAsync(nationalId))
            {
                throw new ArgumentException("National Id already exists");
            }
        }

        public async Task<Professor> CreateAsync(string name, string surname, string nationalId)
        {
            await ValidateStudentAsync(name, surname, nationalId);
            Professor Professor = new Professor(name, surname, nationalId);
            await Database.GetInstance().Professors.CreateAsync(Professor);

            return Professor;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Professors.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            return await Database.GetInstance().Professors.GetAllAsync();
        }

        public async Task<Professor> GetByIdAsync(int id)
        {
            return await Database.GetInstance().Professors.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException("The Id does not correspond to any professor");
        }

        public async Task<Professor> UpdateAsync(int id, string name, string surname, string nationalId)
        {
            Professor professor = await GetByIdAsync(id);

            await ValidateStudentAsync(name, surname, nationalId, professor.NationalId != nationalId);

            professor.NationalId = nationalId;
            professor.Surname = surname;
            professor.Name = name;

            await Database.GetInstance().Professors.UpdateAsync(professor);

            return professor;
        }

    }
}
