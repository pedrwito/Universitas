using Npgsql;
using Universitas.Contracts.Repositories;
using Universitas.Persistance.Repositories;

//Tengo que referenciar el proyecto del cual necesito accerder a clases (o lo que sea).
//Se hace con click derecho en el proyecto actual,
//agregar, agregar dependencia de proyecto y tildar el proyrcto que quiero acceder

//Para agregar npgsql hay que ir al proyecto, adfministrar paquetes de nugget y listyo.
//despues tengo que agregar la libreria con el using (con alt + enter sobre el error me recomienda ponerlo automaticamente)

namespace Universitas.Persistance
{
    public class Database : IDisposable //no me deja hacerla IDisposabe porq es estatica XD
    {
        private static Database? instance = null;
        private readonly NpgsqlDataSource _dataSource;
        public IStudentsRepository Students { get; private set; }
        public ICoursesRepository Courses { get; private set; }
        public IProfessorsRepository Professors { get; private set; }

        private Database()
        {
            _dataSource = NpgsqlDataSource.Create("Host=127.0.0.1;Username=postgres;Password=pedri123;Database=postgres");
            Students = new StudentsRepository(_dataSource);
            Professors = new ProfessorsRepository(_dataSource);
            Courses = new CoursesRepository(_dataSource);
        }

        public void Dispose()
        {
            _dataSource.Dispose();
        }

        public static Database GetInstance()
        {
            if (instance == null)
            {
                instance = new Database();
            }

            return instance;
        }
    }
}
