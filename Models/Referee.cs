using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballScoreApp.Models
{
    public class Referee
    {
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Nationality { get; set; } = string.Empty;
        
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}