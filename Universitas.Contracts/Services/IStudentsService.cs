﻿using Universitas.Contracts.Models;

namespace Universitas.Contracts.Services
{
    public interface IStudentsService
    {
        Task<Student> CreateAsync(string name, string surname, string nationalId);

        Task<Student> UpdateAsync(int id, string name, string surname, string nationalId);

        Task DeleteAsync(int id);

        Task<Student> GetByIdAsync(int id);

        Task<IEnumerable<Student>> GetAllAsync();

    }
}
