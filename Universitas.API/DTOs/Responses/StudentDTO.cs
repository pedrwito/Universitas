namespace Universitas.API.DTOs.Responses
{
    public class StudentDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string NationalId { get; init; }
        public StudentStatusDTO Status;

        public StudentDTO(int id, string name, string surname, string nationalId, StudentStatusDTO status)
        {
            Id = id;
            Name = name;
            Surname = surname;
            NationalId = nationalId;
            Status = status;
        }
    }
}
