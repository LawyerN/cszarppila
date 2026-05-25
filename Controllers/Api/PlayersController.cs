using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballScoreApp.Models;

namespace FootballScoreApp.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly AppDbContext _context;

        public PlayersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Wraca na Dashboard
            }
            ViewBag.Teams = _context.Teams.ToList();
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();

            ViewBag.Teams = _context.Teams.ToList();
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player player)
        {
            if (id != player.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(player);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Teams = _context.Teams.ToList();
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var player = await _context.Players.Include(p => p.Team).FirstOrDefaultAsync(m => m.Id == id);
            if (player == null) return NotFound();

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}