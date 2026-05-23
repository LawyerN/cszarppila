using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballScoreApp.Models
{
    public class Player
    {
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Position { get; set; } = string.Empty;
        
        public int? JerseyNumber { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        [Required]
        public int TeamId { get; set; }
        public Team? Team { get; set; }
        
        public ICollection<PlayerStats>? PlayerStats { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}