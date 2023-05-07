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
    public class MedServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedServices
        public async Task<IActionResult> Index()
        {
            return _context.MedServices != null ?
                        View(await _context.MedServices.Where(ms => ms.isDeleted == false).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.MedServices'  is null.");
        }

        // GET: MedServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,price")] MedService medService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medService);
        }

        // GET: MedServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedServices == null)
            {
                return NotFound();
            }

            var medService = await _context.MedServices.FindAsync(id);
            if (medService == null)
            {
                return NotFound();
            }
            return View(medService);
        }

        // POST: MedServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,price")] MedService medService)
        {
            if (id != medService.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedServiceExists(medService.id))
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
            return View(medService);
        }

        // GET: MedServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedServices == null)
            {
                return NotFound();
            }

            var medService = await _context.MedServices
                .FirstOrDefaultAsync(m => m.id == id);
            if (medService == null)
            {
                return NotFound();
            }

            return View(medService);
        }

        // POST: MedServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedServices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MedServices'  is null.");
            }
            var medService = await _context.MedServices.FindAsync(id);
            if (medService != null)
            {
                _context.MedServices.Remove(medService);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch { }
            return RedirectToAction(nameof(Index));
        }

        private bool MedServiceExists(int id)
        {
            return (_context.MedServices?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
