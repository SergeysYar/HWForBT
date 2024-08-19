using HWForBT.Data;
using HWForBT.Model;
using Microsoft.AspNetCore.Mvc;

namespace HWForBT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly TimesheetDbContext _context;

        public EmployeeController(TimesheetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return _context.GetEmployees();
        }
    }
}
