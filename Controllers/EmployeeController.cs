using AzureDemoAPI.Data;
using AzureDemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzureDemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // INSERT
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee),
                new { id = employee.Id }, employee);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}