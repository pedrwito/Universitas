using Microsoft.AspNetCore.Mvc;
using Universitas.API.DTOs.Auxiliar;
using Universitas.API.DTOs.Requests;
using Universitas.API.DTOs.Responses;
using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Universitas.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService studentsService;

        public StudentsController() 
        {
            studentsService = new StudentsService();
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            return (await studentsService.GetAllAsync()).Select(s => ToDTO(s));
        }

        // GET api/students/5
        [HttpGet("{id}")]
        public async Task<StudentDTO> GetAsync(int id)
        {
            return ToDTO(await studentsService.GetByIdAsync(id));
        }

        // POST api/students
        [HttpPost]
        public async Task<StudentDTO> PostAsync([FromBody] StudentCreateDTO dto)
        {
            return ToDTO(await studentsService.CreateAsync(dto.Name, dto.Surname, dto.NationalId));
        }

        // PUT api/students/5
        [HttpPut("{id}")]
        public async Task<StudentDTO> PutAsync(int id, [FromBody] StudentUpdateDTO dto)
        {
            return ToDTO(await studentsService.UpdateAsync(id, dto.Name, dto.Surname, dto.NationalId, (int)dto.Status));
        }

        // DELETE api/students/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await studentsService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<CourseDTO>> GetCoursesAsync(int id)
        {
            return (await studentsService.GetCoursesAsync(id)).Select(c => CoursesController.ToDTO(c));
        }

        public static StudentDTO ToDTO(Student s)
        {
            return new StudentDTO(s.Id, s.Name, s.Surname, s.NationalId, (StudentStatusDTO)s.Status);
        }
    }
}
