using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class AlumnosRepository : BaseRepository<Alumno>, IAlumnosRepository
    {
        public AlumnosRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Alumno entity)
        {
            string query = "INSERT INTO universidad.alumnos(nombre, apellido, national_id, estado) VALUES($1, $2, $3, $4) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.NationalId, entity.Estado });
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM universidad.alumnos WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { id });
        }

        public async Task<List<Alumno>> GetByApellidoAsync(string apellido)
        {
            string query = "SELECT * FROM universidad.alumnos WHERE apellido ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                         // osea que busca todo lo que empiece con apellido si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { apellido });

            var listaAlumnos = new List<Alumno>();

            while (reader.Read())
            {
                Alumno alumno = MapRowToModel(reader);

                listaAlumnos.Add(alumno);
            }
            return listaAlumnos;
        }

        public override async Task<Alumno?> GetByIdAsync(int id)
        {
            string query = "SELECT (nombre,apellido,national_id,estado,id) FROM universidad.alumnos WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<List<Alumno>> GetByMateriaAsync(Materia materia)
        {
            string query = "SELECT a.* from universidad.alumnos a WHERE " +
                "EXISTS(SELECT am.id_alumno FROM alumnos_en_materias am WHERE am.id_materia = $1 AND am.id_alumno = a.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { materia.Id });

            var listaAlumnos = new List<Alumno>();

            while (reader.Read())
            {
                var alumno = MapRowToModel(reader);

                listaAlumnos.Add(alumno);
            }

            return listaAlumnos;
        }

        public override async Task UpdateAsync(Alumno entity)
        {
            string query = "UPDATE universidad.alumnos SET nombre = $1, apellido = $2, national_id = $3, estado = $4 WHERE id = $5";
            await ExecuteNonQueryAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.NationalId, entity.Estado, entity.Id });
        }

        public async Task<bool> ExistsByNationalIdAsync(string national_id)
        {
            return await ExecuteScalarAsync<bool>("SELECT FROM universidad.alumnos WHERE national_id = $1", new[] { national_id });
        }

        public override async Task<IEnumerable<Alumno>> GetAllAsync()
        {
            string query = "SELECT (nombre,apellido,national_id,estado,id) FROM universidad.alumnos";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query);

            var studentList = new List<Alumno>(); 

            while (reader.Read())
            {
                studentList.Add(MapRowToModel(reader));
            }

            return studentList;
        }

        protected override Alumno MapRowToModel(NpgsqlDataReader reader)
        {
            return new Alumno(
                    (string)reader["nombre"],
                    (string)reader["apellido"],
                    (string)reader["national_id"],
                    (EstadoAlumno)reader["estado"],
                    (int)reader["id"]);
        }
    }
}
