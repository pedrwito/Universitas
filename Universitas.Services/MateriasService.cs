using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Persistance;

namespace Universitas.Services
{
    public class MateriasService : IMateriasService
    {
        private void ValidateCourse(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be empty");
            }  
        }

        public async Task<Materia> CreateAsync(string name)
        {
            ValidateCourse(name);
            Materia Materia = new Materia(name);
            await Database.GetInstance().Materias.CreateAsync(Materia);

            return Materia;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Materias.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await Database.GetInstance().Materias.GetAllAsync();
        }

        public async Task<Materia> GetByIdAsync(int id)
        {
            return await Database.GetInstance().Materias.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException("The Id does not correspond to any course");
        }

        public async Task<Materia> UpdateAsync(int id, string name)
        {
            ValidateCourse(name);

            Materia course = await GetByIdAsync(id);

            course.Nombre = name;

            await Database.GetInstance().Materias.UpdateAsync(course);

            return course;
        }
    }
}
