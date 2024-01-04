namespace Universitas.API.DTOs.Responses
{
    public class ProfessorDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string NationalId { get; init; }

        public ProfessorDTO(int id, string name, string surname, string nationalId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            NationalId = nationalId;
        }
    }
}
