using Dental_Clinic.Context;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Controllers
{
    public class MedCardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedCardsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MedCardController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MedCardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MedCardController/Create
        public ActionResult Create()
        {
            ViewData["Doctorid"] = new SelectList(_context.Doctors, "id", "fullName");
            ViewData["MedTreatmentid"] = new SelectList(_context.MedTreatments, "id", "treatmentName");
            ViewData["Patientid"] = new SelectList(_context.Patients, "id", "fullName");
            ViewData["Diagnosid"] = new SelectList(_context.Diagnosis, "id", "diagnosisName");
            ViewData["Medicationid"] = new SelectList(_context.Medications, "id", "name");
            return View();
        }

        // POST: MedCardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedCardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MedCardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedCardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedCardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
