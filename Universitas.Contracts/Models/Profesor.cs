namespace Universitas.Contracts.Models
{
    public class Profesor : Persona
    {
        public Profesor(string nombre, string apellido, string nationalId, int? ID = null) : base(nombre, apellido, nationalId, ID)
        {

        }
    }
}
