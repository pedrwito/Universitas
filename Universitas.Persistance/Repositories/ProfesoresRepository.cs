using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class ProfesoresRepository : BaseRepository<Profesor>, IProfesoresRepository
    {
        public ProfesoresRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Profesor entity)
        {
            string query = "INSERT INTO universidad.profesores(nombre, apellido, national_id) VALUES($1, $2, $3) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.NationalId });
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM universidad.profesores WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { id });
        }

        public async Task<List<Profesor>> GetByApellidoAsync(string apellido)
        {
            string query = "SELECT * FROM universidad.profesores WHERE apellido ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                            // osea que busca todo lo que empiece con apellido si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { apellido });

            var listaProfesores = new List<Profesor>();

            while (reader.Read())
            {
                Profesor profesor = MapRowToModel(reader);

                listaProfesores.Add(profesor);
            }

            return listaProfesores;
        }

        public override async Task<Profesor?> GetByIdAsync(int id)
        {
            string query = "SELECT (nombre,apellido,national_id,id) FROM universidad.profesores WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<List<Profesor>> GetByMateriaAsync(Materia materia)
        {
            string query = "SELECT a.* from universidad.profesores a WHERE " +
                "EXISTS(SELECT am.id_profesor FROM profesores_en_materias am WHERE am.id_materia = $1 AND am.id_profesor = a.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { materia.Id });

            var listaProfesores = new List<Profesor>();

            while (reader.Read())
            {
                var profesor = MapRowToModel(reader);

                listaProfesores.Add(profesor);
            }

            return listaProfesores;
        }

        public override async Task UpdateAsync(Profesor entity)
        {
            string query = "UPDATE universidad.profesores SET nombre = $1, apellido = $2, national_id = $3, estado = $4 WHERE id = $5";
            await ExecuteNonQueryAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.NationalId, entity.Id });
        }

        public async Task<bool> ExistsByNationalIdAsync(string national_id)
        {
            return await ExecuteScalarAsync<bool>("SELECT FROM universidad.profesores WHERE national_id = $1", new[] { national_id });
        }

        public override async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            string query = "SELECT (nombre,apellido,national_id,estado,id) FROM universidad.alumnos";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query);

            var professorList = new List<Profesor>();

            while (reader.Read())
            {
                professorList.Add(MapRowToModel(reader));
            }

            return professorList;
        }

        protected override Profesor MapRowToModel(NpgsqlDataReader reader)
        {
            return new Profesor(
                    (string)reader["nombre"],
                    (string)reader["apellido"],
                    (string)reader["national_id"],
                    (int)reader["id"]);
        }
    }
}

