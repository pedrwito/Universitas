using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class ProfesoresRepository : RepositoryDB<Profesor>, IProfesoresRepository
    {
        public ProfesoresRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Profesor entity)
        {
            string query = "INSERT INTO universidad.profesores(nombre, apellido, dni) VALUES($1, $2, $3) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.DNI });
            entity.ID = ID;
        }

        public override async Task DeleteAsync(Profesor entity)
        {
            string query = "DELETE FROM universidad.profesores WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { entity.ID });
        }

        public async Task<List<Profesor>> GetByApellidoAsync(string apellido)
        {
            string query = "SELECT * FROM universidad.profesores WHERE apellido ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                            // osea que busca todo lo que empiece con apellido si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { apellido });

            var listaProfesores = new List<Profesor>();

            while (reader.Read())
            {
                Profesor profesor = new Profesor(
                    (string)reader["nombre"],
                    (string)reader["apellido"],
                    (int)reader["dni"],
                    (int)reader["id"]);

                listaProfesores.Add(profesor);
            }

            return listaProfesores;
        }

        public async Task<Profesor?> GetByIdAsync(int id)
        {
            string query = "SELECT (nombre,apellido,dni,id) FROM universidad.profesores WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            reader.Read();

            if (reader.Read())
            {
                return new Profesor(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3));
            }

            return null;
        }

        public async Task<List<Profesor>> GetByMateriaAsync(Materia materia)
        {
            string query = "SELECT a.* from universidad.profesores a WHERE " +
                "EXISTS(SELECT am.id_profesor FROM profesores_en_materias am WHERE am.id_materia = $1 AND am.id_profesor = a.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { materia.ID });

            var listaProfesores = new List<Profesor>();

            while (reader.Read())
            {
                var profesor = new Profesor(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(4));

                listaProfesores.Add(profesor);
            }

            return listaProfesores;
        }

        public override async Task UpdateAsync(Profesor entity)
        {
            string query = "UPDATE universidad.profesores SET nombre = $1, apellido = $2, dni = $3, estado = $4 WHERE id = $5";
            await ExecuteNonQueryAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.DNI, entity.ID });
        }
    }
}