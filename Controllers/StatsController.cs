using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballScoreApp.Models;

namespace FootballScoreApp.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly AppDbContext _context;

        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Stats/TopScorers
        public async Task<IActionResult> TopScorers(string sortBy = "goals")
        {
            var scorersData = await _context.PlayerStats
                .Include(ps => ps.Player)
                .ThenInclude(p => p!.Team)
                .GroupBy(ps => new { ps.Player!.Id, ps.Player.FirstName, ps.Player.LastName, TeamName = ps.Player.Team!.Name })
                .Select(g => new TopScorerViewModel
                {
                    PlayerName = g.Key.FirstName + " " + g.Key.LastName,
                    TeamName = g.Key.TeamName,
                    Goals = g.Sum(ps => ps.Goals),
                    Assists = g.Sum(ps => ps.Assists)
                })
                .ToListAsync();

            var sortedScorers = sortBy == "assists" 
                ? scorersData.OrderByDescending(x => x.Assists).ToList() 
                : scorersData.OrderByDescending(x => x.Goals).ToList();

            ViewBag.SortBy = sortBy;
            return View(sortedScorers);
        }

        // GET: /Stats/StadiumGoals
        public async Task<IActionResult> StadiumGoals()
        {
            var stadiumStats = await _context.Matches
                .Include(m => m.Stadium)
                .Where(m => m.IsCompleted)
                .GroupBy(m => new { m.Stadium!.Id, m.Stadium.Name })
                .Select(g => new StadiumStatsViewModel
                {
                    StadiumName = g.Key.Name,
                    TotalGoals = g.Sum(m => m.HomeScore + m.AwayScore),
                    MatchesCount = g.Count()
                })
                .OrderByDescending(x => x.TotalGoals)
                .ToListAsync();

            return View(stadiumStats);
        }

        // GET: /Stats/Referees
        public async Task<IActionResult> Referees()
        {
            var refereeStats = await _context.Matches
                .Include(m => m.Referee)
                .Where(m => m.RefereeId != null)
                .GroupBy(m => new { m.Referee!.Id, m.Referee.FirstName, m.Referee.LastName })
                .Select(g => new RefereeStatsViewModel
                {
                    Name = g.Key.FirstName + " " + g.Key.LastName,
                    MatchesOfficiated = g.Count()
                })
                .OrderByDescending(x => x.MatchesOfficiated)
                .ToListAsync();

            return View(refereeStats);
        }
    }

    public class TopScorerViewModel
    {
        public string PlayerName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public int Goals { get; set; }
        public int Assists { get; set; }
    }

    public class StadiumStatsViewModel
    {
        public string StadiumName { get; set; } = string.Empty;
        public int TotalGoals { get; set; }
        public int MatchesCount { get; set; }
    }

    public class RefereeStatsViewModel
    {
        public string Name { get; set; } = string.Empty;
        public int MatchesOfficiated { get; set; }
    }
}