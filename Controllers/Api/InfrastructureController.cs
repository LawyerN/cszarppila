using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            ViewBag.Stadiums = await _context.Stadiums.ToListAsync();
            ViewBag.Referees = await _context.Referees.ToListAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateStadium() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStadium(Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stadium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stadium);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStadium(int id)
        {
            var stadium = await _context.Stadiums.FindAsync(id);
            if (stadium != null)
            {
                _context.Stadiums.Remove(stadium);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateReferee() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateReferee(Referee referee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referee);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReferee(int id)
        {
            var referee = await _context.Referees.FindAsync(id);
            if (referee != null)
            {
                _context.Referees.Remove(referee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}