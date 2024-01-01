namespace Universitas.Contracts.Models
{
    public class Student : Person
    {
        public StudentStatus Status;

        public Student(string name, string surname, string nationalId) : base(name, surname, nationalId, null)
        {
            this.Status = StudentStatus.Active;
        }

        public Student(string name, string surname, string nationalId, StudentStatus status, int id) : base(name, surname, nationalId, id)
        {
            this.Status = status;
            this.Id = id;
        }
    }
}
