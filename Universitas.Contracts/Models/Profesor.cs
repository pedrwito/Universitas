namespace Universitas.Contracts.Models
{
    public class Profesor : Persona
    {
        public Profesor(string nombre, string apellido, int dni, int? ID = null) : base(nombre, apellido, dni, ID)
        {

        }
    }
}
