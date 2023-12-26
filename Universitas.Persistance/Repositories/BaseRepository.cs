﻿using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universitas.Persistance.Repositories
{
    internal abstract class RepositoryDB<T> : IRepository<T>
    {
        private NpgsqlDataSource _dataSource;

        public RepositoryDB(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public abstract Task CreateAsync(T entity);
        public abstract Task DeleteAsync(T entity);
        public abstract Task UpdateAsync(T entity);

        protected async Task<int> ExecuteNonQueryAsync(string query, object[] parameters)
        {
            using (NpgsqlCommand command = _dataSource.CreateCommand(query)) //using es basicamente hcaer que el objeto exista en memoria hasta
                                                                             //que se cierre la llave. Luego se llama a dispose para cerrarlo/sacarlo de memoria (Si una clase implementa IDisposable,
                                                                             //se tiene que llamar a dispose despues de usar al objeto para liquidarlo).
            {
                command.Parameters.AddRange(parameters);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo ejecutar la query");
                }

                return rowsAffected;
            }
        }

        protected async Task<RetType> ExecuteScalarAsync<RetType>(string query, object[] parameters)
        {

            using (NpgsqlCommand command = _dataSource.CreateCommand(query))
            {
                command.Parameters.AddRange(parameters);

                RetType? scalar = (RetType?)await command.ExecuteScalarAsync();

                if (scalar == null)
                {
                    throw new Exception("No se pudo ejecutar la query");
                }

                return scalar;
            }
        }

        protected async Task<int> ExecuteScalarIntAsync(string query, object[] parameters)
        {
            return await ExecuteScalarAsync<int>(query, parameters);
        }

        protected async Task<NpgsqlDataReader> GetQueryReaderAsync(string query, object[] parameters)
        {
            using (NpgsqlCommand command = _dataSource.CreateCommand(query))
            {
                command.Parameters.AddRange(parameters);

                return await command.ExecuteReaderAsync();
            }
        }
    }
}