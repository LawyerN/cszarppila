using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballScoreApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string ShortName { get; set; } = string.Empty;
        
        public int? Founded { get; set; }
        
        public int? StadiumId { get; set; }
        public Stadium? Stadium { get; set; }
        
        public ICollection<Player>? Players { get; set; }
        public ICollection<Match>? HomeMatches { get; set; }
        public ICollection<Match>? AwayMatches { get; set; }
    }
}