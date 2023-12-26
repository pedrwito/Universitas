using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universitas.Contracts.Models
{
    public class Alumno : Persona
    {
        public EstadoAlumno Estado;
        public Alumno(string nombre, string apellido, int dni) : base(nombre, apellido, dni, null)
        {
            this.Estado = EstadoAlumno.Activo;

        }

        public Alumno(string nombre, string apellido, int dni, EstadoAlumno estado, int id) : base(nombre, apellido, dni, id)
        {
            this.Estado = estado;
            this.ID = id;
        }

    }
}
