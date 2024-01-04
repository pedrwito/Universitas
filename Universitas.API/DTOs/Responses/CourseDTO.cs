namespace Universitas.API.DTOs.Responses
{
    public class CourseDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public CourseDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
