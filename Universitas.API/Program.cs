using Universitas.API.Middlewares;

namespace Universitas.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //injeccion de dependencias ---> Le paso la interfaz y la clase y se va a poder usar siempre en toda clase que instancie ASP.NET (por ej los controladores). Es como una "instanciacion global"
            //            builder.Services.AddTransient<IAlumnoService, AlumnoService>() 
            //Tambien hay que injectar todo lo que requiere la dependencia que injecte para existir. Por ej NpgSqlDataSource para AlumnoService

            // Hay tres tios de instanciacion: - Scoped: (cada vez que me refiera a una interfaz que esta como scoped, se crea una nueva instancia antes de llamar al constructor).
            //                                 - Transient (el mas comun): Vamos a crear una instancia de este servicio POR CADA REQUEST que hace el usuario.
            //                                 - Singleton: Se crea una sola instancia a lo largo de todo el programa. NO se borra de memoria al dejar e referenciarse (en los otros si)

            //TODO injectar el NpgSqlDataSource

   
            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();  // Agrego el middleware a la lista de middlewares que se ejecutan en cada request. Puedo tener muchos y en orden.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}