using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Context;
using Dental_Clinic.Models;

namespace Dental_Clinic.Views
{
    public class ListPrepforTreatmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListPrepforTreatmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ListPrepforTreatments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ListPrepforTreatments.Where(lpt => lpt.isDeleted == false).Include(l => l.MedTreatment).Include(l => l.Medication);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ListPrepforTreatments/Create
        public IActionResult Create()
        {
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName");
            ViewData["Medicationid"] = new SelectList(_context.Medications, "id", "name");
            return View();
        }

        // POST: ListPrepforTreatments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,MedTreatmentid,Medicationid,amountMedications")] ListPrepforTreatment listPrepforTreatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listPrepforTreatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName", listPrepforTreatment.MedTreatmentid);
            ViewData["Medicationid"] = new SelectList(_context.Medications, "id", "name", listPrepforTreatment.Medicationid);
            return View(listPrepforTreatment);
        }

        // GET: ListPrepforTreatments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListPrepforTreatments == null)
            {
                return NotFound();
            }

            var listPrepforTreatment = await _context.ListPrepforTreatments.FindAsync(id);
            if (listPrepforTreatment == null)
            {
                return NotFound();
            }
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName", listPrepforTreatment.MedTreatmentid);
            ViewData["Medicationid"] = new SelectList(_context.Medications, "id", "name", listPrepforTreatment.Medicationid);
            return View(listPrepforTreatment);
        }

        // POST: ListPrepforTreatments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,MedTreatmentid,Medicationid,amountMedications")] ListPrepforTreatment listPrepforTreatment)
        {
            if (id != listPrepforTreatment.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listPrepforTreatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListPrepforTreatmentExists(listPrepforTreatment.id))
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
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName", listPrepforTreatment.MedTreatmentid);
            ViewData["Medicationid"] = new SelectList(_context.Medications, "id", "name", listPrepforTreatment.Medicationid);
            return View(listPrepforTreatment);
        }

        // GET: ListPrepforTreatments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListPrepforTreatments == null)
            {
                return NotFound();
            }

            var listPrepforTreatment = await _context.ListPrepforTreatments
                .Include(l => l.MedTreatment)
                .Include(l => l.Medication)
                .FirstOrDefaultAsync(m => m.id == id);
            if (listPrepforTreatment == null)
            {
                return NotFound();
            }

            return View(listPrepforTreatment);
        }

        // POST: ListPrepforTreatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListPrepforTreatments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ListPrepforTreatments'  is null.");
            }
            var listPrepforTreatment = await _context.ListPrepforTreatments.FindAsync(id);
            if (listPrepforTreatment != null)
            {
                _context.ListPrepforTreatments.Remove(listPrepforTreatment);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch {}
            return RedirectToAction(nameof(Index));
        }

        private bool ListPrepforTreatmentExists(int id)
        {
          return (_context.ListPrepforTreatments?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
