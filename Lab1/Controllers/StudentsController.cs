using Lab1.Data;
using Lab1.DTOs;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.DTOs;
using Lab1.Models;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students.Select(s => new StudentDTO
            {
                Name = s.Name,
                Email = s.Email
            }));
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(new StudentDTO
            {
                Name = student.Name,
                Email = student.Email
            });
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO studentDTO)
        {
            var student = new Student
            {
                Name = studentDTO.Name,
                Email = studentDTO.Email
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Trả về status 201 Created và URL của resource mới tạo
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, studentDTO);
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDTO studentDTO)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = studentDTO.Name;
            student.Email = studentDTO.Email;

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Trả về status 204 No Content (cập nhật thành công)
            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            // Trả về status 204 No Content (xóa thành công)
            return NoContent();
        }
    }
}