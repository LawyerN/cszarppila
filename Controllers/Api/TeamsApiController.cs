using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballScoreApp.Models;
using FootballScoreApp.Filters;
using Microsoft.AspNetCore.Authorization;

namespace FootballScoreApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuth] // <-- Nasz filtr zabezpieczający całe API
    public class TeamsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TeamsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            return await _context.Teams
                .Select(t => ItemToDTO(t))
                .ToListAsync();
        }

        // GET: api/TeamsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return NotFound();

            return ItemToDTO(team);
        }

        // POST: api/TeamsApi
        [HttpPost]
        public async Task<ActionResult<TeamDTO>> PostTeam(TeamDTO teamDTO)
        {
            var team = new Team
            {
                Name = teamDTO.Name,
                ShortName = teamDTO.ShortName,
                Founded = teamDTO.Founded,
                StadiumId = teamDTO.StadiumId
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, ItemToDTO(team));
        }

        // PUT: api/TeamsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, TeamDTO teamDTO)
        {
            if (id != teamDTO.Id) return BadRequest();

            var team = await _context.Teams.FindAsync(id);
            if (team == null) return NotFound();

            // Aktualizujemy tylko bezpieczne pola (ochrona przed over-posting)
            team.Name = teamDTO.Name;
            team.ShortName = teamDTO.ShortName;
            team.Founded = teamDTO.Founded;
            team.StadiumId = teamDTO.StadiumId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teams.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/TeamsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return NotFound();

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Mapowanie encji na bezpieczne DTO
        private static TeamDTO ItemToDTO(Team team) => new TeamDTO
        {
            Id = team.Id,
            Name = team.Name,
            ShortName = team.ShortName,
            Founded = team.Founded,
            StadiumId = team.StadiumId
        };
    }
}