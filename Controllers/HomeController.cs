using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballScoreApp.Models;

namespace FootballScoreApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortBy = "goals")
{
    // 1. Poprawny Król Strzelców (grupujemy po Id i polach fizycznych, FullName sklejamy po select)
    var scorersData = await _context.PlayerStats
        .Include(ps => ps.Player)
        .ThenInclude(p => p.Team)
        .GroupBy(ps => new { ps.Player.Id, ps.Player.FirstName, ps.Player.LastName, TeamName = ps.Player.Team.Name })
        .Select(g => new {
            PlayerName = g.Key.FirstName + " " + g.Key.LastName,
            TeamName = g.Key.TeamName,
            Goals = g.Sum(ps => ps.Goals),
            Assists = g.Sum(ps => ps.Assists)
        })
        .ToListAsync();

    // Sortowanie danych w pamięci (zabezpiecza przed problemami z translacją SQL)
    ViewBag.TopScorers = sortBy == "assists" 
        ? scorersData.OrderByDescending(x => x.Assists).Take(5).ToList() 
        : scorersData.OrderByDescending(x => x.Goals).Take(5).ToList();

    // 2. Poprawny Ranking Stadionów (grupujemy po polach istniejących)
    ViewBag.StadiumStats = await _context.Matches
        .Include(m => m.Stadium)
        .Where(m => m.IsCompleted)
        .GroupBy(m => new { m.Stadium.Id, m.Stadium.Name })
        .Select(g => new {
            StadiumName = g.Key.Name,
            TotalGoals = g.Sum(m => m.HomeScore + m.AwayScore),
            MatchesCount = g.Count()
        })
        .OrderByDescending(x => x.TotalGoals)
        .ToListAsync();

    // 3. Poprawna Aktywność Sędziów (nie używamy FullName w GroupBy!)
    ViewBag.RefereeStats = await _context.Matches
        .Include(m => m.Referee)
        .Where(m => m.RefereeId != null)
        .GroupBy(m => new { m.Referee.Id, m.Referee.FirstName, m.Referee.LastName })
        .Select(g => new {
            Name = g.Key.FirstName + " " + g.Key.LastName,
            MatchesOfficiated = g.Count()
        })
        .OrderByDescending(x => x.MatchesOfficiated)
        .ToListAsync();

    // 4. Pobranie danych do tabel (Zarządzanie) - bez zmian
    ViewBag.AllPlayers = await _context.Players.Include(p => p.Team).OrderBy(p => p.LastName).ToListAsync();
    ViewBag.AllTeams = await _context.Teams.Include(t => t.Stadium).ToListAsync();
    
    var matches = await _context.Matches.Include(m => m.HomeTeam).Include(m => m.AwayTeam).OrderByDescending(m => m.MatchDate).ToListAsync();
    ViewBag.AllMatches = matches;
    ViewBag.AllStadiums = await _context.Stadiums.ToListAsync();
    ViewBag.AllReferees = await _context.Referees.ToListAsync();
    ViewBag.AllMatches = matches;
    return View();
}
    }
}