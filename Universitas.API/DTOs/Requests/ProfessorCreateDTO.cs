// using System.ComponentModel.DataAnnotations; TODO investigar

namespace Universitas.API.DTOs.Requests
{
    public class ProfessorCreateDTO
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string NationalId { get; init; }

        public ProfessorCreateDTO(string name, string surname, string nationalId)
        {
            Name = name;
            Surname = surname;
            NationalId = nationalId;
        }
    }
}
