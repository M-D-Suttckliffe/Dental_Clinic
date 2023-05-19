using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Context;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Dental_Clinic.Controllers
{
    [Authorize(Roles = "Doctor, HeadDoctor")]
    public class MedTreatmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedTreatmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedTreatments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MedTreatments.Where(mt => mt.isDeleted == false).Include(m => m.Diagnos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MedTreatments/Create
        public IActionResult Create()
        {
            ViewData["Diagnosid"] = new SelectList(_context.Diagnosis, "id", "diagnosisName");
            return View();
        }

        // POST: MedTreatments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Diagnosid,treatmentName")] MedTreatment medTreatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medTreatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Diagnosid"] = new SelectList(_context.Diagnosis, "id", "diagnosisName", medTreatment.Diagnosid);
            return View(medTreatment);
        }

        // GET: MedTreatments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedTreatments == null)
            {
                return NotFound();
            }

            var medTreatment = await _context.MedTreatments.FindAsync(id);
            if (medTreatment == null)
            {
                return NotFound();
            }
            ViewData["Diagnosid"] = new SelectList(_context.Diagnosis, "id", "diagnosisName", medTreatment.Diagnosid);
            return View(medTreatment);
        }

        // POST: MedTreatments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Diagnosid,treatmentName")] MedTreatment medTreatment)
        {
            if (id != medTreatment.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medTreatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedTreatmentExists(medTreatment.id))
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
            ViewData["Diagnosid"] = new SelectList(_context.Diagnosis, "id", "diagnosisName", medTreatment.Diagnosid);
            return View(medTreatment);
        }

        // GET: MedTreatments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedTreatments == null)
            {
                return NotFound();
            }

            var medTreatment = await _context.MedTreatments
                .Include(m => m.Diagnos)
                .FirstOrDefaultAsync(m => m.id == id);
            if (medTreatment == null)
            {
                return NotFound();
            }

            return View(medTreatment);
        }

        // POST: MedTreatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedTreatments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MedTreatments'  is null.");
            }
            var medTreatment = await _context.MedTreatments.FindAsync(id);
            if (medTreatment != null)
            {
                _context.MedTreatments.Remove(medTreatment);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch { }
            return RedirectToAction(nameof(Index));
        }

        private bool MedTreatmentExists(int id)
        {
            return (_context.MedTreatments?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
