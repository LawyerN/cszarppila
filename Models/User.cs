using System;
using System.ComponentModel.DataAnnotations;

namespace FootballScoreApp.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required, StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        public string ApiToken { get; set; } = Guid.NewGuid().ToString("N");
        
        public bool IsAdmin { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}