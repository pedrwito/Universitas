using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class ProfessorsRepository : BaseRepository<Professor>, IProfessorsRepository
    {
        public ProfessorsRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Professor entity)
        {
            string query = "INSERT INTO universidad.professors(name, surname, national_id) VALUES($1, $2, $3) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Name, entity.Surname, entity.NationalId });
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM universidad.professors WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { id });
        }

        public async Task<List<Professor>> GetBySurnameAsync(string surname)
        {
            string query = "SELECT * FROM universidad.professors WHERE surname ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                            // osea que busca todo lo que empiece con surname si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { surname });

            var professorsList = new List<Professor>();

            while (reader.Read())
            {
                Professor Professor = MapRowToModel(reader);

                professorsList.Add(Professor);
            }

            return professorsList;
        }

        public override async Task<Professor?> GetByIdAsync(int id)
        {
            string query = "SELECT (name,surname,national_id,id) FROM universidad.professors WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<List<Professor>> GetByCourseAsync(Course course)
        {
            string query = "SELECT p.* from universidad.professors p WHERE " +
                "EXISTS(SELECT pc.id_professor FROM professors_in_courses pc WHERE am.id_course = $1 AND pc.id_professor = p.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { course.Id });

            var listaprofessors = new List<Professor>();

            while (reader.Read())
            {
                var Professor = MapRowToModel(reader);

                listaprofessors.Add(Professor);
            }

            return listaprofessors;
        }

        public override async Task UpdateAsync(Professor entity)
        {
            string query = "UPDATE universidad.professors SET name = $1, surname = $2, national_id = $3, state = $4 WHERE id = $5";
            await ExecuteNonQueryAsync(query, new object[] { entity.Name, entity.Surname, entity.NationalId, entity.Id });
        }

        public async Task<bool> ExistsByNationalIdAsync(string national_id)
        {
            return await ExecuteScalarAsync<bool>("SELECT FROM universidad.professors WHERE national_id = $1", new[] { national_id });
        }

        public override async Task<IEnumerable<Professor>> GetAllAsync()
        {
            string query = "SELECT (name,surname,national_id,estado,id) FROM universidad.professors";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query);

            var professorsList = new List<Professor>();

            while (reader.Read())
            {
                professorsList.Add(MapRowToModel(reader));
            }

            return professorsList;
        }

        protected override Professor MapRowToModel(NpgsqlDataReader reader)
        {
            return new Professor(
                    (string)reader["name"],
                    (string)reader["surname"],
                    (string)reader["national_id"],
                    (int)reader["id"]);
        }
    }
}

