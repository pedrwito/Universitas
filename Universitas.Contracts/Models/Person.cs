namespace Universitas.Contracts.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalId { get; set; }

        public Person(string name, string surname, string nationalId, int? id = null)
        {
            this.Name = name;
            this.Surname = surname;
            this.NationalId = nationalId;

            this.Id = id ?? 0; // si es distinto de null poner id, sino lo que hay del otro lado del ??
        }
    }
}
