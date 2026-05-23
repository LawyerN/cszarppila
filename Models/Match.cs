using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballScoreApp.Models
{
    public class Match
    {
        public int Id { get; set; }
        
        [Required]
        public int HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; }
        
        [Required]
        public int AwayTeamId { get; set; }
        public Team? AwayTeam { get; set; }
        
        [Required]
        public DateTime MatchDate { get; set; }
        
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        
        public int? RefereeId { get; set; }
        public Referee? Referee { get; set; }
        
        public int? StadiumId { get; set; }
        public Stadium? Stadium { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public ICollection<PlayerStats>? PlayerStats { get; set; }
    }
}