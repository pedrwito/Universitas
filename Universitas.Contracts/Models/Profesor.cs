using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universitas.Contracts.Models
{
    public class Profesor : Persona
    {
        public Profesor(string nombre, string apellido, int dni, int? ID = null) : base(nombre, apellido, dni, ID)
        {

        }
    }
}
