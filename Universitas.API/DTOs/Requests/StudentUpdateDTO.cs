// using System.ComponentModel.DataAnnotations; TODO investigar

using Universitas.API.DTOs.Auxiliar;

namespace Universitas.API.DTOs.Requests
{
    public class StudentUpdateDTO
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string NationalId { get; init; }
        public StudentStatusDTO Status { get; init; }

        public StudentUpdateDTO(string name, string surname, string nationalId, StudentStatusDTO status)
        {
            Name = name;
            Surname = surname;
            NationalId = nationalId;
            Status = status;
        }
    }
}
