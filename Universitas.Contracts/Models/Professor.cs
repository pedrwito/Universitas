namespace Universitas.Contracts.Models
{
    public class Professor : Person
    {
        public Professor(string name, string surname, string nationalId, int? ID = null) : base(name, surname, nationalId, ID)
        {

        }
    }
}
