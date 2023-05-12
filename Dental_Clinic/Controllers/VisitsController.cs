using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Context;
using Dental_Clinic.Models;

namespace Dental_Clinic.Controllers
{
    public class VisitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Doctor)
                .Include(v => v.MedTreatment)
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        public IActionResult Create()
        {
            ViewData["Doctorid"] = new SelectList(_context.Doctors, "id", "fullName");
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName");
            ViewData["Patientid"] = new SelectList(_context.Patients, "id", "fullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Patientid,MedTreatmentid,Doctorid,dateVisit,status,isDeleted")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Doctorid"] = new SelectList(_context.Doctors, "id", "fullName", visit.Doctorid);
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName", visit.MedTreatmentid);
            ViewData["Patientid"] = new SelectList(_context.Patients, "id", "fullName", visit.Patientid);
            return View(visit);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewData["Doctorid"] = new SelectList(_context.Doctors, "id", "fullName", visit.Doctorid);
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName", visit.MedTreatmentid);
            ViewData["Patientid"] = new SelectList(_context.Patients, "id", "fullName", visit.Patientid);
            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Patientid,MedTreatmentid,Doctorid,dateVisit,status,isDeleted")] Visit visit)
        {
            if (id != visit.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.id))
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
            ViewData["Doctorid"] = new SelectList(_context.Doctors, "id", "fullName", visit.Doctorid);
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "fullName", "treatmentName", visit.MedTreatmentid);
            ViewData["Patientid"] = new SelectList(_context.Patients, "id", "fullName", visit.Patientid);
            return View(visit);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Doctor)
                .Include(v => v.MedTreatment)
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Visits'  is null.");
            }
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                _context.Visits.Remove(visit);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch { }
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return (_context.Visits?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
