using Microsoft.AspNetCore.Mvc;
using Universitas.API.DTOs.Requests;
using Universitas.API.DTOs.Responses;
using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Universitas.API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService coursesService;

        public CoursesController() 
        {
            coursesService = new CoursesService();
        }

        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            return (await coursesService.GetAllAsync()).Select(c => ToDTO(c));
        }

        // GET api/<UniversitasController>/5
        [HttpGet("{id}")]
        public async Task<CourseDTO> GetAsync(int id)
        {
            return ToDTO(await coursesService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<CourseDTO> PostAsync([FromBody] CourseCreateDTO dto)
        {
            return ToDTO(await coursesService.CreateAsync(dto.Name));
        }

        [HttpPut("{id}")]
        public async Task<CourseDTO> PutAsync(int id, [FromBody] CourseCreateDTO dto)
        {
            return ToDTO(await coursesService.UpdateAsync(id, dto.Name));
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await coursesService.DeleteAsync(id);
        }

        [HttpPost("{courseId}/students")]
        public async Task EnrollStudent(int courseId, [FromBody] StudentEnrollDTO dto)
        {
            await coursesService.EnrollStudentAsync(courseId, dto.Id);
        }

        [HttpPost("{courseId}/professors")]
        public async Task EnrollProfessor(int courseId, [FromBody] ProfessorEnrollDTO dto)
        {
            await coursesService.EnrollProfessorAsync(courseId, dto.Id);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<StudentDTO>> GetStudentsAsync(int id)
        {
            return (await coursesService.GetStudentsAsync(id)).Select(s => StudentsController.ToDTO(s));
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ProfessorDTO>> GetProfessorsAsync(int id)
        {
            return (await coursesService.GetProfessorsAsync(id)).Select(p => ProfessorsController.ToDTO(p));
        }

        public static CourseDTO ToDTO(Course c)
        {
            return new CourseDTO(c.Id, c.Name);
        }
    }
}
