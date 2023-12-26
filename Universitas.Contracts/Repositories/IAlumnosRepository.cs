using Universitas.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universitas.Contracts.Repositories
{
    public interface IAlumnosRepository : IRepository<Alumno>
    {
        public Task<Alumno?> GetByIdAsync(int id);

        public Task<List<Alumno>> GetByApellidoAsync(string apellido);

        public Task<List<Alumno>> GetByMateriaAsync(Materia materia);
    }
}
