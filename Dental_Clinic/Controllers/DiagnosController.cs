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
    public class DiagnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diagnos
        public async Task<IActionResult> Index()
        {
            return _context.Diagnosis != null ?
                        View(await _context.Diagnosis.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Diagnosis'  is null.");
        }

        // GET: Diagnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagnosis == null)
            {
                return NotFound();
            }

            var diagnos = await _context.Diagnosis
                .FirstOrDefaultAsync(m => m.id == id);
            if (diagnos == null)
            {
                return NotFound();
            }

            return View(diagnos);
        }

        // GET: Diagnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diagnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,diagnosisName")] Diagnos diagnos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnos);
        }

        // GET: Diagnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagnosis == null)
            {
                return NotFound();
            }

            var diagnos = await _context.Diagnosis.FindAsync(id);
            if (diagnos == null)
            {
                return NotFound();
            }
            return View(diagnos);
        }

        // POST: Diagnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,diagnosisName")] Diagnos diagnos)
        {
            if (id != diagnos.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosExists(diagnos.id))
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
            return View(diagnos);
        }

        // GET: Diagnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagnosis == null)
            {
                return NotFound();
            }

            var diagnos = await _context.Diagnosis
                .FirstOrDefaultAsync(m => m.id == id);
            if (diagnos == null)
            {
                return NotFound();
            }

            return View(diagnos);
        }

        // POST: Diagnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diagnosis == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Diagnosis'  is null.");
            }
            var diagnos = await _context.Diagnosis.FindAsync(id);
            if (diagnos != null)
            {
                _context.Diagnosis.Remove(diagnos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosExists(int id)
        {
            return (_context.Diagnosis?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
