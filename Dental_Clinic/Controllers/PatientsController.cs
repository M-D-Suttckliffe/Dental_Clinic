using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Context;
using Dental_Clinic.Models;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Dental_Clinic.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class GenderName
        {
            public int id { get; set; }
            public string genderName { get; set; }
        }
        List<GenderName> genderNames = new List<GenderName>();

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return _context.Patients != null ?
                        View(await _context.Patients.Where(p => p.isDeleted == false).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Patients'  is null.");
        }
        
        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["gender"] = new SelectList(new[]
            {
                new SelectListItem("Не указано", "2"),
                new SelectListItem("Мужской", "0"),
                new SelectListItem("Женский", "1")
            }, "Value", "Text");
            //ViewData["gender"] = new SelectList(genders, "id", "name");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,surName,name,middleName,birthday,gender,phoneNumber,address")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["gender"] = new SelectList(new[]
            {
                new SelectListItem("Не указано", "2"),
                new SelectListItem("Мужской", "0"),
                new SelectListItem("Женский", "1")
            }, "Value", "Text", patient.gender);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["gender"] = new SelectList(new[]
            {
                new SelectListItem("Не указано", "2"),
                new SelectListItem("Мужской", "0"),
                new SelectListItem("Женский", "1")
            }, "Value", "Text", patient.gender);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,surName,name,middleName,birthday,gender,phoneNumber,address")] Patient patient)
        {
            if (id != patient.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["gender"] = new SelectList(new[]
            {
                new SelectListItem("Не указано", "2"),
                new SelectListItem("Мужской", "0"),
                new SelectListItem("Женский", "1")
            }, "Value", "Text", patient.gender);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch {}
            
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return (_context.Patients?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
