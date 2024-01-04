// using System.ComponentModel.DataAnnotations; TODO investigar

namespace Universitas.API.DTOs.Requests
{
    public class CourseCreateDTO
    {
        public string Name { get; init; }

        public CourseCreateDTO(string name)
        {
            Name = name;
        }
    }
}
