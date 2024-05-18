using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Test;
using Api_Test.Context;
using System.Reflection;
using System.Linq.Expressions;
using Api_Test.Predicate;


namespace Api_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        [HttpGet("populate-data")]
        public async Task<IActionResult> ReadFileAsync()
        {
            if (_context.Students is not null && _context.Students.Any())
            {
                return Ok("no need to populate data");
            }

            try
            {
                string filePath = @"C:\StudentsList.txt";

                using StreamReader sr = new StreamReader(filePath);

                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] studentsLine = line.Split(',');

                    var indexedStrings = studentsLine.Select((str, index) => (str, index));

                    Student newStudent = new Student();

                    foreach (var (strs, index) in indexedStrings)
                    {
                        switch (index) 
                        {
                            case 0: newStudent.Name = strs; break;
                            case 1: newStudent.Gender = strs.ToCharArray()[0]; break;
                            case 2: newStudent.Age = int.TryParse(strs, out int age) ? age : 0; break;
                            case 3: newStudent.Education = strs; break;
                            case 4: newStudent.AcademicYear = int.TryParse(strs, out int ayear) ? ayear : 0; break;
                        }

                    }

                    _context.Students?.Add(newStudent);

                }

                await _context.SaveChangesAsync();

                return Ok("populate data done");
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            return await _context.Students.ToListAsync();
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByFilter(VmFindStudents vmFilter)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var predicate = PredicateBuilder.True<Student>();

            if (vmFilter.Gender == 'F' || vmFilter.Gender == 'M') 
                predicate = vmFilter.isGenderAnd ? predicate.And(e => e.Gender == vmFilter.Gender) : predicate.Or(e => e.Gender == vmFilter.Gender);

            if (vmFilter.Age > 0)
                predicate = vmFilter.isAgeAnd ? predicate.And(e => e.Age == vmFilter.Age) : predicate.Or(e => e.Age == vmFilter.Age);

            if (!string.IsNullOrEmpty(vmFilter.Education))
                predicate = vmFilter.isEducationAnd ? predicate.And(e => e.Education == vmFilter.Education) : predicate.Or(e => e.Education == vmFilter.Education);

            if (vmFilter.AcademicYear > 0)
                predicate = vmFilter.isAcademicYearAnd ? predicate.And(e => e.AcademicYear == vmFilter.AcademicYear) : predicate.Or(e => e.AcademicYear == vmFilter.AcademicYear);

            return await _context.Students.Where(predicate).ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'AppDbContext.Students' is null.");
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();

            return Ok("Editing done correctly");
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(VmAddStudent vmStudent)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'AppDbContext.Students' is null.");
            }

            var realStudent = new Student(vmStudent);
            _context.Students.Add(realStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = realStudent.Id }, realStudent);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'AppDbContext.Students' is null.");
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            //return NoContent();

            return Ok("Deletion done correctly");
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
