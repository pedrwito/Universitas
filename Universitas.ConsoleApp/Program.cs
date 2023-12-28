using Universitas.Contracts.Models;

namespace Universitas.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Student pedro = new Student("pedro","barrera","1235678");


            //Database.GetInstance().Alumnos.CreateAsync(pedro);
        }
    }
}