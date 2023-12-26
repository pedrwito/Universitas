namespace Universitas.Contracts.Models
{
    public class Materia
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public Materia(string nombre, int? id = null)
        {
            this.Nombre = nombre;
            if (id != null)
            {
                this.ID = id.Value;
            }
        }
    }
}
