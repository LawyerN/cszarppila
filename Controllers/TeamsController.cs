using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballScoreApp.Models;

namespace FootballScoreApp.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.Include(t => t.Stadium).ToListAsync();
            return View(teams);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewBag.Stadiums = _context.Stadiums.ToList();
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Stadiums = _context.Stadiums.ToList();
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var team = await _context.Teams.FindAsync(id);
            if (team == null) return NotFound();

            ViewBag.Stadiums = _context.Stadiums.ToList();
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Stadiums = _context.Stadiums.ToList();
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var team = await _context.Teams.Include(t => t.Stadium).FirstOrDefaultAsync(m => m.Id == id);
            if (team == null) return NotFound();

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}