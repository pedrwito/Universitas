using Universitas.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universitas.Contracts.Repositories
{
    public interface IProfesoresRepository : IRepository<Profesor>
    {
        public Task<Profesor?> GetByIdAsync(int id);

        public Task<List<Profesor>> GetByApellidoAsync(string apellido);
    }
}
