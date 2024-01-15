using Microsoft.AspNetCore.Mvc;
using Universitas.API.DTOs.Requests;
using Universitas.API.DTOs.Responses;
using Universitas.Contracts.Models;
using Universitas.Contracts.Services;
using Universitas.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Universitas.API.Controllers
{
    [Route("api/professors")]
    [ApiController]
    public class ProfessorsController : ControllerBase 
    {
        private readonly IProfessorsService professorsService;

        public ProfessorsController() 
        {
            professorsService = new ProfessorsService();
        }

        [HttpGet]
        public async Task<IEnumerable<ProfessorDTO>> GetAllAsync()
        {
            return (await professorsService.GetAllAsync()).Select(p => ToDTO(p));
        }

        // GET api/<UniversitasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorDTO>> GetAsync(int id)
        {
            try
            {
                return ToDTO(await professorsService.GetByIdAsync(id));
            } catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<UniversitasController>
        [HttpPost]
        public async Task<ProfessorDTO> PostAsync([FromBody] ProfessorCreateDTO dto)
        {
            return ToDTO(await professorsService.CreateAsync(dto.Name, dto.Surname, dto.NationalId));
        }

        // PUT api/<UniversitasController>/5
        [HttpPut("{id}")]
        public async Task<ProfessorDTO> PutAsync(int id, [FromBody] ProfessorCreateDTO dto)
        {
            return ToDTO(await professorsService.UpdateAsync(id, dto.Name, dto.Surname, dto.NationalId));
        }

        // DELETE api/<UniversitasController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await professorsService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<CourseDTO>> GetCoursesAsync(int id)
        {
            return (await professorsService.GetCoursesAsync(id)).Select(c => CoursesController.ToDTO(c));
        }

        public static ProfessorDTO ToDTO(Professor p)
        {
            return new ProfessorDTO(p.Id, p.Name, p.Surname, p.NationalId);
        }
    }
}
