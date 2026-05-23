using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballScoreApp.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required, StringLength(100)]
        public string City { get; set; } = string.Empty;
        
        public int Capacity { get; set; }
        
        public ICollection<Team>? Teams { get; set; }
    }
}