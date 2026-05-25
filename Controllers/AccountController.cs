using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FootballScoreApp.Models;

namespace FootballScoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Jeśli użytkownik jest już zalogowany, nie ma sensu pokazywać mu logowania
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken] // Zabezpieczenie przed atakami CSRF (zgodnie z notatkami z wykładu)
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Podaj login i hasło.";
                return View();
            }

            // Szukamy użytkownika w bazie
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                // Weryfikacja hasha
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                
                if (result == PasswordVerificationResult.Success)
                {
                    // Budujemy tzw. Claims, czyli informacje o zalogowanym użytkowniku (sesja)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("ApiToken", user.ApiToken),
                        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Rejestracja logowania (tworzy ciasteczko autoryzacyjne)
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Nieprawidłowy login lub hasło.";
            return View();
        }

        // GET/POST: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}