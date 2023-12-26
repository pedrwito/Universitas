namespace Universitas.Contracts.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NationalId { get; set; }

        public Persona(string nombre, string apellido, string nationalId, int? id = null)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.NationalId = nationalId;

            this.Id = id ?? 0; // si es distinto de null poner id, sino lo que hay del otro lado del ??
        }
    }
}
