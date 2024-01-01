using Microsoft.AspNetCore.Mvc;
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

        private static StudentDTO ToDTO(Student s)
        {
            return new StudentDTO(s.Id, s.Name, s.Surname, s.NationalId, (StudentStatusDTO)s.Status);
        }

        // GET api/<UniversitasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UniversitasController>
        [HttpPost]
        public async Task<StudentDTO> PostAsync([FromBody] StudentCreateDTO dto)
        {
            return ToDTO(await studentsService.CreateAsync(dto.Name, dto.Surname, dto.NationalId));
        }

        // PUT api/<UniversitasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UniversitasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
