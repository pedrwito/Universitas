using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class MateriasRepository : BaseRepository<Materia>, IMateriasRepository
    {
        public MateriasRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Materia entity)
        {
            string query = "INSERT INTO universidad.materias(nombre) VALUES($1) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Nombre });
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM universidad.materias WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { id });
        }

        public async Task<List<Materia>> GetByNombreAsync(string nombre)
        {
            string query = "SELECT * FROM universidad.materias WHERE apellido ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                          // osea que busca todo lo que empiece con apellido si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { nombre });

            var listaMaterias = new List<Materia>();
            while (reader.Read())
            {
                Materia materia = new Materia(
                    (string)reader["nombre"],
                    (int)reader["id"]);

                listaMaterias.Add(materia);
            }
            return listaMaterias;
        }

        public override async Task<Materia?> GetByIdAsync(int id)
        {
            string query = "SELECT (nombre,id) FROM universidad.materias WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<List<Materia>> GetMateriasAlumnoAsync(Alumno alumno)
        {
            string query = "SELECT m.* from universidad.materias a WHERE " +
                "EXISTS(SELECT am.id_materia FROM alumnos_en_materias am WHERE am.id_alumno = $1 AND am.id_materia = m.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { alumno.Id });

            var listaMaterias = new List<Materia>();

            while (reader.Read())
            {
                var materia = MapRowToModel(reader);

                listaMaterias.Add(materia);
            }

            return listaMaterias;
        }

        public override async Task UpdateAsync(Materia entity)
        {
            string query = "UPDATE universidad.materias SET nombre = $1 WHERE id = $2";
            await ExecuteNonQueryAsync(query, new object[] { entity.Nombre, entity.Id });
        }

        public override async Task<IEnumerable<Materia>> GetAllAsync()
        {
            string query = "SELECT (nombre,apellido,national_id,estado,id) FROM universidad.materias";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query);

            var coursestList = new List<Materia>();

            while (reader.Read())
            {
                coursestList.Add(MapRowToModel(reader));
            }

            return coursestList;
        }

        protected override Materia MapRowToModel(NpgsqlDataReader reader)
        {
            return new Materia(
                    (string)reader["nombre"],
                    (int)reader["id"]);
        }
    }
}
