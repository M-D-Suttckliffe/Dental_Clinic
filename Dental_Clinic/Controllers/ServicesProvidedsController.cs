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
    public class ServicesProvidedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesProvidedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ServicesProvideds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ServicesProvideds.Where(sp => sp.isDeleted == false).Include(s => s.MedService).Include(s => s.Visit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ServicesProvideds/Create
        public IActionResult Create()
        {
            ViewData["MedServiceid"] = new SelectList(_context.MedServices, "id", "id");
            ViewData["Visitid"] = new SelectList(_context.Visits, "id", "id");
            return View();
        }

        // POST: ServicesProvideds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Visitid,MedServiceid")] ServicesProvided servicesProvided)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicesProvided);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedServiceid"] = new SelectList(_context.MedServices, "id", "id", servicesProvided.MedServiceid);
            ViewData["Visitid"] = new SelectList(_context.Visits, "id", "id", servicesProvided.Visitid);
            return View(servicesProvided);
        }

        // GET: ServicesProvideds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServicesProvideds == null)
            {
                return NotFound();
            }

            var servicesProvided = await _context.ServicesProvideds.FindAsync(id);
            if (servicesProvided == null)
            {
                return NotFound();
            }
            ViewData["MedServiceid"] = new SelectList(_context.MedServices, "id", "id", servicesProvided.MedServiceid);
            ViewData["Visitid"] = new SelectList(_context.Visits, "id", "id", servicesProvided.Visitid);
            return View(servicesProvided);
        }

        // POST: ServicesProvideds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Visitid,MedServiceid")] ServicesProvided servicesProvided)
        {
            if (id != servicesProvided.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicesProvided);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesProvidedExists(servicesProvided.id))
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
            ViewData["MedServiceid"] = new SelectList(_context.MedServices, "id", "id", servicesProvided.MedServiceid);
            ViewData["Visitid"] = new SelectList(_context.Visits, "id", "id", servicesProvided.Visitid);
            return View(servicesProvided);
        }

        // GET: ServicesProvideds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServicesProvideds == null)
            {
                return NotFound();
            }

            var servicesProvided = await _context.ServicesProvideds
                .Include(s => s.MedService)
                .Include(s => s.Visit)
                .FirstOrDefaultAsync(m => m.id == id);
            if (servicesProvided == null)
            {
                return NotFound();
            }

            return View(servicesProvided);
        }

        // POST: ServicesProvideds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServicesProvideds == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ServicesProvideds'  is null.");
            }
            var servicesProvided = await _context.ServicesProvideds.FindAsync(id);
            if (servicesProvided != null)
            {
                _context.ServicesProvideds.Remove(servicesProvided);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch { }
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesProvidedExists(int id)
        {
            return (_context.ServicesProvideds?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
