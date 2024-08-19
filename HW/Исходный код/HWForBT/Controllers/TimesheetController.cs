using HWForBT.Data;
using HWForBT.Model;
using Microsoft.AspNetCore.Mvc;

namespace HWForBT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimesheetController : ControllerBase
    {
        private readonly TimesheetDbContext _context;

        public TimesheetController(TimesheetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> GetTimesheets()
        {
            var employees = _context.GetEmployees();
            var timesheets = _context.GetTimesheets().Select(t => new {
                t.Id,
                EmployeeName = employees.FirstOrDefault(e => e.Id == t.Employee)?.FirstName + " " + employees.FirstOrDefault(e => e.Id == t.Employee)?.LastName,
                t.Reason,
                t.StartDate,
                t.Duration,
                t.Discounted,
                t.Description
            });
            return Ok(timesheets);
        }

        [HttpGet("{id}")]
        public ActionResult<Timesheet> GetTimesheet(int id)
        {
            var timesheet = _context.GetTimesheetById(id);
            if (timesheet == null)
            {
                return NotFound();
            }
            return timesheet;
        }

        [HttpPost]
        public ActionResult<Timesheet> CreateTimesheet(Timesheet timesheet)
        {
            _context.AddTimesheet(timesheet);
            return CreatedAtAction(nameof(GetTimesheet), new { id = timesheet.Id }, timesheet);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTimesheet(int id, Timesheet timesheet)
        {
            if (id != timesheet.Id)
            {
                return BadRequest();
            }

            _context.UpdateTimesheet(timesheet);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTimesheet(int id)
        {
            _context.DeleteTimesheet(id);
            return NoContent();
        }
    }
}
