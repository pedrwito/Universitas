using Npgsql;
using Universitas.Contracts.Models;
using Universitas.Contracts.Repositories;

namespace Universitas.Persistance.Repositories
{
    internal class CoursesRepository : BaseRepository<Course>, ICoursesRepository
    {
        public CoursesRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task CreateAsync(Course entity)
        {
            string query = "INSERT INTO universidad.courses(name) VALUES($1) RETURNING id";
            int ID = await ExecuteScalarIntAsync(query, new object[] { entity.Name });
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM university.courses WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { id });
        }

        public async Task<List<Course>> GetByNameAsync(string name)
        {
            string query = "SELECT * FROM universidad.courses WHERE name ILIKE $1%"; // ILIKE me busca cosas parecidas (no es case sensitive, LIKE hace algo parecido pero es case sensitive y mas performante) y % es un comodin
                                                                                          // osea que busca todo lo que empiece con apellido si pongo % al final (por ejemplo bar encontraria barrera, barrientos, etc)

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { name });

            var listaCourses = new List<Course>();

            while (reader.Read())
            {
                listaCourses.Add(MapRowToModel(reader));
            }
            return listaCourses;
        }

        public override async Task<Course?> GetByIdAsync(int id)
        {
            string query = "SELECT (name,id) FROM universidad.courses WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<List<Course>> GetCoursesStudentAsync(Student student)
        {
            string query = "SELECT s.* from universidad.courses s WHERE " +
                "EXISTS(SELECT sc.id_course FROM students_in_courses am WHERE sc.id_student = $1 AND sc.id_course = s.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { student.Id });

            var listaCourses = new List<Course>();

            while (reader.Read())
            {
                var Course = MapRowToModel(reader);

                listaCourses.Add(Course);
            }

            return listaCourses;
        }

        public override async Task UpdateAsync(Course entity)
        {
            string query = "UPDATE universidad.courses SET name = $1 WHERE id = $2";
            await ExecuteNonQueryAsync(query, new object[] { entity.Name, entity.Id });
        }

        public override async Task<IEnumerable<Course>> GetAllAsync()
        {
            string query = "SELECT (name,id) FROM universidad.courses";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query);

            var coursesList = new List<Course>();

            while (reader.Read())
            {
                coursesList.Add(MapRowToModel(reader));
            }

            return coursesList;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT * FROM universidad.courses WHERE id = $1)", new object[] { id });
        }

        protected override Course MapRowToModel(NpgsqlDataReader reader)
        {
            return new Course(
                    (string)reader["name"],
                    (int)reader["id"]);
        }

        public async Task<List<Course>> GetByStudentAsync(int studentID)
        {
            string query = "SELECT C.* from universidad.courses c WHERE " +
                "EXISTS(SELECT sc.id_student FROM students_in_courses sc WHERE sc.id_student = $1 AND sc.id_course = c.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { studentID });

            var coursesList = new List<Course>();

            while (reader.Read())
            {
                var course = MapRowToModel(reader);

                coursesList.Add(course);
            }

            return coursesList;
        }

        public async Task<List<Course>> GetByProfessorAsync(int professorID)
        {
            string query = "SELECT c.* from universidad.courses c WHERE " +
                "EXISTS(SELECT pc.id_course FROM professors_in_courses pc WHERE pc.id_professor = $1 AND pc.course = c.id)";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { professorID });

            var coursesList = new List<Course>();

            while (reader.Read())
            {
                var course = MapRowToModel(reader);

                coursesList.Add(course);
            }

            return coursesList;
        }

        public async Task EnrollStudentAsync(int studentId, int courseId)
        {
            string query = "INSERT INTO universidad.students_in_courses(id_student, id_course) VALUES($1,$2)";
            await ExecuteNonQueryAsync(query, new object[] { studentId, courseId });
        }

        public async Task EnrollProfessorAsync(int professorId, int courseId)
        {
            string query = "INSERT INTO universidad.professors_in_courses(id_professor, id_course) VALUES($1,$2)";
            await ExecuteNonQueryAsync(query, new object[] { professorId, courseId });
        }

        public Task RemoveProfessorAsync(int professorId, int courseId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveStudentAsync(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }
    }
}
