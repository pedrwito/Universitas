using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class StudentsRepository : BaseRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Student entity)
        {
            string query = "INSERT INTO universidad.students(name, surname, national_id, status) VALUES($1, $2, $3, $4) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Name, entity.Surname, entity.NationalId, (int)entity.Status });
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM universidad.students WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { id });
        }

        public async Task<List<Student>> GetBySurnameAsync(string surname)
        {
            string query = "SELECT * FROM universidad.students WHERE surname ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                         // osea que busca todo lo que empiece con surname si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { surname });

            var studentsList = new List<Student>();

            while (reader.Read())
            {
                Student student = MapRowToModel(reader);

                studentsList.Add(student);
            }
            return studentsList;
        }

        public override async Task<Student?> GetByIdAsync(int id)
        {
            string query = "SELECT (name,surname,national_id,status,id) FROM universidad.students WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<List<Student>> GetByCourseAsync(Course course)
        {
            string query = "SELECT s.* from universidad.students s WHERE " +
                "EXISTS(SELECT sc.id_alumno FROM alumnos_in_courses sc WHERE sc.id_course = $1 AND sc.id_student = s.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { course.Id });

            var studentsList = new List<Student>();

            while (reader.Read())
            {
                var student = MapRowToModel(reader);

                studentsList.Add(student);
            }

            return studentsList;
        }

        public override async Task UpdateAsync(Student entity)
        {
            string query = "UPDATE universidad.students SET name = $1, surname = $2, national_id = $3, status = $4 WHERE id = $5";
            await ExecuteNonQueryAsync(query, new object[] { entity.Name, entity.Surname, entity.NationalId, entity.Status, entity.Id });
        }

        public async Task<bool> ExistsByNationalIdAsync(string national_id)
        {
            return await ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT * FROM universidad.students WHERE national_id = $1)", new[] { national_id });
        }

        public override async Task<IEnumerable<Student>> GetAllAsync()
        {
            string query = "SELECT name, surname, national_id, status, id FROM universidad.students";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query);

            var studentList = new List<Student>(); 

            while (reader.Read())
            {
                studentList.Add(MapRowToModel(reader));
            }

            return studentList;
        }

        protected override Student MapRowToModel(NpgsqlDataReader reader)
        {
            return new Student(
                    (string)reader["name"],
                    (string)reader["surname"],
                    (string)reader["national_id"],
                    (StudentStatus)reader["status"],
                    (int)reader["id"]);
        }

        public async Task<int> GetAmountOfCorsesEnrolled(Student alumno)
        {
            string query = "SELECT COUNT(a.*) FROM alumnos_en_Course a WHERE a.id_alumno = $1";

            return await ExecuteScalarIntAsync(query, new object[] {alumno.Id});


        }
    }
}
