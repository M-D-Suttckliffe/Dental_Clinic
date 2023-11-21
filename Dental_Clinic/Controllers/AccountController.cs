using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dental_Clinic.Models;
using Dental_Clinic.Context;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dental_Clinic.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = _context.Doctors.FirstOrDefault(u => u.login == model.login && u.password == model.Password);
                if (doctor != null)
                {
                    if(_context.Posts.FirstOrDefault(u=> u.id == doctor.Postid).postName == "Главный-врач")
                        await Authenticate(model.login,"HeadDoctor" , model.isRemember);
                    else
                        await Authenticate(model.login, "Doctor", model.isRemember);
                    return RedirectToAction("Index", "Home");
                }
                if (_context.Patients.FirstOrDefault(u => u.login == model.login && u.password == model.Password) != null)
                {
                    await Authenticate(model.login, "Patient", model.isRemember);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["gender"] = new SelectList(new[]
            {
                new SelectListItem("Не указано", "2"),
                new SelectListItem("Мужской", "0"),
                new SelectListItem("Женский", "1")
            }, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("id,surName,name,middleName,birthday,gender,phoneNumber,address, Login, Password, ConfirmPassword")] Register model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Doctors.FirstOrDefault(u => u.login == model.Login) == null || _context.Patients.FirstOrDefault(u => u.login == model.Login) == null)
                {
                    // добавляем пользователя в бд
                    _context.Patients.Add(new Patient { surName = model.surName, name = model.name, middleName = model.middleName, birthday = model.birthday, gender = model.gender, phoneNumber = model.phoneNumber, address = model.address, login = model.Login, password = model.Password });
                    await _context.SaveChangesAsync();
                    await Authenticate(model.Login, "Patient"); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            ViewData["gender"] = new SelectList(new[]
            {
                new SelectListItem("Не указано", "2"),
                new SelectListItem("Мужской", "0"),
                new SelectListItem("Женский", "1")
            }, "Value", "Text", model.gender);
            return View(model);
        }

        private async Task Authenticate(string userName,string userType, bool toPresistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, userType)
            };
            // создаем объект ClaimsIdentity
  
            // установка аутентификационных куки
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = toPresistent
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
