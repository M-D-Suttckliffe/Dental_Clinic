using Dental_Clinic.Context;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dental_Clinic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JSONVisitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JSONVisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetVisits()
        {
            var visits = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.servicesProvideds).ThenInclude(m => m.MedService).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            //var json = JsonConvert.SerializeObject(visits, Formatting.Indented);
            if (visits == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Посещения не были найдены");
            }
            return StatusCode(StatusCodes.Status200OK, visits);
        }

        [HttpGet("id")]
        public IActionResult GetVisits(int id)
        {
            var visit = _context.Visits.Where(v => v.isDeleted == false && v.id == id).Include(v => v.servicesProvideds).ThenInclude(m => m.MedService).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            //var json = JsonConvert.SerializeObject(visits, Formatting.Indented);
            if (visit == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"Посещение с {id} не было найдено");
            }
            return StatusCode(StatusCodes.Status200OK, visit);
        }

        [HttpPatch]
        public IActionResult PostVisit(Visit visit)
        {
            var dbVisits = _context.Visits.Add(visit);
            if (dbVisits == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"Посещение по дате {visit.dateVisit} не получилось добавить");
            }
            return StatusCode(StatusCodes.Status200OK, dbVisits);
        }
    }
}
