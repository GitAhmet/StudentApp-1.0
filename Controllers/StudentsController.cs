
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Models;

namespace StudentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentAppContext _context;

    public StudentsController(StudentAppContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<Students>> GetStudent()
    {
        var result = await _context.Student.ToListAsync();
        return Ok(result);
    }

    [HttpGet("GetAsId/")]
    public async Task<ActionResult<Students>> GetAsId(int id)
    {
        var result = await _context.Student.FirstOrDefaultAsync(m => m.Id == id);
        if (result is null)
            return NotFound($"Wrong Id {id}");
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> AddStudent(AddStudentRequest addStudentRequest)
    {
        var student = new Students()
        {
            UserName = addStudentRequest.UserName,
            FirstName = addStudentRequest.FirstName,
            LastName = addStudentRequest.LastName,
            Address = addStudentRequest.Address,
            TlfNo = addStudentRequest.TlfNo,
            School = addStudentRequest.School
        };
        await _context.Student.AddAsync(student);
        await _context.SaveChangesAsync();

        return Ok(student);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateStudent([FromRoute] int id, UpdateStudentRequest updateStudentRequest)
    {
        var result = await _context.Student.FindAsync(id);

        if (result != null)
        {
            result.UserName = updateStudentRequest.UserName;
            result.FirstName = updateStudentRequest.FirstName;
            result.LastName = updateStudentRequest.LastName;
            result.Address = updateStudentRequest.Address;
            result.TlfNo = updateStudentRequest.TlfNo;
            result.School = updateStudentRequest.School;

            await _context.SaveChangesAsync();

            return Ok(result);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteStudent([FromRoute] int id)
    {
        var result = await _context.Student.FindAsync(id);
        if (result != null)
        {
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        return NotFound();
    }

}
