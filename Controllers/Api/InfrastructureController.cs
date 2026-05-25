using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FootballScoreApp.Models;

namespace FootballScoreApp.Controllers
{
    [Authorize]
    public class InfrastructureController : Controller
    {
        private readonly AppDbContext _context;

        public InfrastructureController(AppDbContext context)
        {
            _context = context;
        }

        // --- STADIONY ---
        [HttpGet]
        public IActionResult CreateStadium() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStadium(Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                _context.Stadiums.Add(stadium);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(stadium);
        }

        // --- SĘDZIOWIE ---
        [HttpGet]
        public IActionResult CreateReferee() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReferee(Referee referee)
        {
            if (ModelState.IsValid)
            {
                _context.Referees.Add(referee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(referee);
        }
    }
}