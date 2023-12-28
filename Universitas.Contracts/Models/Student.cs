namespace Universitas.Contracts.Models
{
    public class Student : Person
    {
        public StudentStatus State;

        public Student(string name, string surname, string nationalId) : base(name, surname, nationalId, null)
        {
            this.State = StudentStatus.Active;
        }

        public Student(string name, string surname, string nationalId, StudentStatus state, int id) : base(name, surname, nationalId, id)
        {
            this.State = state;
            this.Id = id;
        }
    }
}
