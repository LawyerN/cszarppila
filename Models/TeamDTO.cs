namespace FootballScoreApp.Models
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public int? Founded { get; set; }
        public int? StadiumId { get; set; }
    }
}