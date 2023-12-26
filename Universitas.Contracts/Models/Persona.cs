using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universitas.Contracts.Models
{
    public class Persona
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }


        public Persona(string nombre, string apellido, int dni, int? ID = null)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.DNI = dni;
            if (ID.HasValue)
            {
                this.ID = ID.Value;
            }
        }
    }
}
