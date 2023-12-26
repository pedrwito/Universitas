namespace Universitas.Contracts.Models
{
    public class Alumno : Persona
    {
        public EstadoAlumno Estado;

        public Alumno(string nombre, string apellido, string nationalId) : base(nombre, apellido, nationalId, null)
        {
            this.Estado = EstadoAlumno.Activo;
        }

        public Alumno(string nombre, string apellido, string nationalId, EstadoAlumno estado, int id) : base(nombre, apellido, nationalId, id)
        {
            this.Estado = estado;
            this.Id = id;
        }
    }
}
