using Universitas.Contracts.Models;

namespace Universitas.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Alumno pedro = new Alumno("pedro","barrera",1235678);


            //Database.GetInstance().Alumnos.CreateAsync(pedro);
        }
    }
}