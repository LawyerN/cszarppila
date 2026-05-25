using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FootballScoreApp.Models;
using System; // Wymagane dla Guid

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
        [ValidateAntiForgeryToken] // Zabezpieczenie przed atakami CSRF
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

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Podaj login i hasło.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Podane hasła nie są identyczne.";
                return View();
            }

            // Sprawdzamy, czy użytkownik o takim loginie już istnieje
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (existingUser != null)
            {
                ViewBag.Error = "Użytkownik o podanej nazwie już istnieje.";
                return View();
            }

            // Tworzymy nowego użytkownika
            var newUser = new User
            {
                Username = username,
                IsAdmin = false, // Domyślnie nowy użytkownik nie jest administratorem
                ApiToken = Guid.NewGuid().ToString() // Generujemy unikalny token API dla użytkownika
            };

            // Haszowanie hasła i przypisanie do encji
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);

            // Zapis w bazie danych
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Po pomyślnej rejestracji przekierowujemy na stronę logowania (lub można od razu zalogować)
            return RedirectToAction("Login", "Account");
        }

        // GET/POST: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}