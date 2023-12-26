using Universitas.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universitas.Contracts.Repositories
{
    public interface IRepository <T>
    {
        public Task CreateAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
