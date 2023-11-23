using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Context;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using CsvHelper;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;
using MimeKit;
using MailKit.Net.Smtp;

namespace Dental_Clinic.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Patient"))
            {
                var applicationDbContextPat = _context.Visits.Where(v => v.isDeleted == false && v.Patient.login == User.Identity.Name).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
                return View(await applicationDbContextPat.ToListAsync());
            }
            var applicationDbContext = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult CreateFileCSV()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var visits = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            var builder = new StringBuilder();
            builder.AppendLine("Дата посещения,Статус,ФИО Пациента,ФИО Врача,Лечение");
            foreach (var v in visits)
            {
                builder.AppendLine($"{v.dateVisit},{v.status},{v.Patient.fullName},{v.Doctor.fullName},{v.MedTreatment.treatmentName}");
            }
            return File(Encoding.GetEncoding(1251).GetBytes(builder.ToString()), "text/csv", "Visits.csv");
        }

        [HttpGet]
        public IActionResult CreateFileTXT()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var visits = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.servicesProvideds).ThenInclude(m => m.MedService).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            var builder = new StringBuilder();
            foreach (var v in visits)
            {
                builder.AppendLine($"Запись:\n   {v.dateVisit}\n    {v.Patient.fullName}\n    {v.Doctor.fullName}\n    {v.MedTreatment.treatmentName}\n   Записи об услугах:");
                foreach (var s in v.servicesProvideds)
                {
                    builder.AppendLine($"       {s.MedService.name}");
                }
                builder.AppendLine("   Конец записи об услугах");
                builder.AppendLine("Конец записи");
            }
            return File(Encoding.GetEncoding(1251).GetBytes(builder.ToString()), "text/txt", "Visits.txt");
        }
        [HttpGet]
        public IActionResult CreateFileXML()
        {
            var visits = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.servicesProvideds).ThenInclude(m => m.MedService).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(ms, xws))
            {
                var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Visits"));

                foreach (var visit in visits)
                {
                    doc.Root.Add(new XElement("Visit", new XAttribute("Id", visit.id),
                        new XElement("DateVisit", visit.dateVisit),
                        new XElement("Doctor", visit.Doctor.fullName, new XAttribute("Id", visit.Doctorid)),
                        new XElement("Patient", visit.Patient.fullName, new XAttribute("Id", visit.Patientid)),
                        new XElement("MedTreatment", visit.MedTreatment.treatmentName, new XAttribute("Id", visit.MedTreatmentid)),
                        new XElement("servicesProvideds")
                        ));
                        IEnumerable<XElement> categories = doc.Root.Elements("Visit").Where(c => (string)c.Attribute("Id") == $"{visit.id}");
                        foreach (var services in visit.servicesProvideds)
                        {
                            categories.First().Add(new XElement("Service", services.MedService.name, new XAttribute("id", services.id)));
                        }
                }
                doc.WriteTo(xw);
            }
            ms.Position = 0;
            return File(ms, "text/xml", "Visits.xml");
        }
        [HttpGet]
        public IActionResult VisitInformation()
        {
            var visits = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.servicesProvideds).ThenInclude(m => m.MedService).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            var json = JsonConvert.SerializeObject(visits, Newtonsoft.Json.Formatting.Indented);

            return Ok(json);
        }
        [HttpGet]
        public IActionResult CreateFileJSON()
        {
            var visits = _context.Visits.Where(v => v.isDeleted == false).Include(v => v.servicesProvideds).ThenInclude(m => m.MedService).Include(v => v.Doctor).Include(v => v.MedTreatment).Include(v => v.Patient);
            var json = JsonConvert.SerializeObject(visits, Newtonsoft.Json.Formatting.Indented);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Me", "m.d.suttckliffe@gmail.com"));
            message.To.Add(new MailboxAddress("FMe", "m.d.suttckliffe@gmail.com"));
            message.Subject = "Test";
            message.Body = new TextPart("plain") {Text = json };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("m.d.suttckliffe@gmail.com", "muzo wzur byhg luct");
                client.Send(message);
                
                client.Disconnect(true);
            }

            return RedirectToAction("Index");
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
