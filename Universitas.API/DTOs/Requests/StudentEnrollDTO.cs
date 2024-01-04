namespace Universitas.API.DTOs.Requests
{
    public class StudentEnrollDTO
    {
        public int Id { get; init; }

        public StudentEnrollDTO(int id)
        {
            Id = id;
        }
    }
}
