using Dental_Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Dental_Clinic.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Internal;

namespace Dental_Clinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        //private readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        
        [Authorize]
        public IActionResult Index()
        {
            //int cr = 2;
            //var cout = new Microsoft.Data.SqlClient.SqlParameter("sp_bill", System.Data.SqlDbType.Int);
            //cout.Direction = System.Data.ParameterDirection.Output;
            //var c =  _context.Database.SqlQuery<int>($"SELECT GetBillServicesprovided(2)");



            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}