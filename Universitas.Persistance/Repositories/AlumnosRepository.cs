using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class AlumnosRepository : RepositoryDB<Alumno>, IAlumnosRepository
    {
        public AlumnosRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Alumno entity)
        {
            string query = "INSERT INTO universidad.alumnos(nombre, apellido, dni, estado) VALUES($1, $2, $3, $4) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.DNI, entity.Estado });
            entity.ID = ID;
        }

        public override async Task DeleteAsync(Alumno entity)
        {
            string query = "DELETE FROM universidad.alumnos WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { entity.ID });
        }

        public async Task<List<Alumno>> GetByApellidoAsync(string apellido)
        {
            string query = "SELECT * FROM universidad.alumnos WHERE apellido ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                         // osea que busca todo lo que empiece con apellido si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { apellido });

            var listaAlumnos = new List<Alumno>();
            while (reader.Read())
            {
                Alumno alumno = new Alumno(
                    (string)reader["nombre"],
                    (string)reader["apellido"],
                    (int)reader["dni"],
                    (EstadoAlumno)reader["estado"],
                    (int)reader["id"]);

                listaAlumnos.Add(alumno);
            }
            return listaAlumnos;
        }

        public async Task<Alumno?> GetByIdAsync(int id)
        {
            string query = "SELECT (nombre,apellido,dni,estado,id) FROM universidad.alumnos WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            reader.Read();

            if (reader.Read())
            {
                return new Alumno(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    (EstadoAlumno)reader.GetInt32(3),
                    reader.GetInt32(4));
            }

            return null;
        }

        public async Task<List<Alumno>> GetByMateriaAsync(Materia materia)
        {
            string query = "SELECT a.* from universidad.alumnos a WHERE " +
                "EXISTS(SELECT am.id_alumno FROM alumnos_en_materias am WHERE am.id_materia = $1 AND am.id_alumno = a.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { materia.ID });

            var listaAlumnos = new List<Alumno>();

            while (reader.Read())
            {
                var alumno = new Alumno(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    (EstadoAlumno)reader.GetInt32(3),
                    reader.GetInt32(4));

                listaAlumnos.Add(alumno);
            }

            return listaAlumnos;
        }

        public override async Task UpdateAsync(Alumno entity)
        {
            string query = "UPDATE universidad.alumnos SET nombre = $1, apellido = $2, dni = $3, estado = $4 WHERE id = $5";
            await ExecuteNonQueryAsync(query, new object[] { entity.Nombre, entity.Apellido, entity.DNI, entity.Estado, entity.ID });
        }
    }
}
