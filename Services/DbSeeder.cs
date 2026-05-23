using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FootballScoreApp.Models;

namespace FootballScoreApp.Services
{
    public static class DbSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider, string adminUsername, string adminPassword)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
            
            if (context.Teams.Any()) return;
            
            Console.WriteLine("Seeding database with La Liga data...");
            
            var adminUser = new User
            {
                Username = adminUsername,
                PasswordHash = PasswordHasher.HashPassword(adminPassword),
                ApiToken = Guid.NewGuid().ToString("N"),
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow
            };
            context.Users.Add(adminUser);
            
            var campNou = new Stadium { Name = "Spotify Camp Nou", City = "Barcelona", Capacity = 99354 };
            var santiagoBernabeu = new Stadium { Name = "Santiago Bernabéu", City = "Madrid", Capacity = 81044 };
            var wandaMetropolitano = new Stadium { Name = "Cívitas Metropolitano", City = "Madrid", Capacity = 68456 };
            var ramonSanchezPizjuan = new Stadium { Name = "Ramón Sánchez Pizjuán", City = "Sevilla", Capacity = 43883 };
            var mestalla = new Stadium { Name = "Mestalla", City = "Valencia", Capacity = 49430 };
            
            context.Stadiums.AddRange(campNou, santiagoBernabeu, wandaMetropolitano, ramonSanchezPizjuan, mestalla);
            context.SaveChanges();
            
            var barcelona = new Team 
            { 
                Name = "FC Barcelona", 
                ShortName = "BAR", 
                Founded = 1899, 
                StadiumId = campNou.Id 
            };
            var realMadrid = new Team 
            { 
                Name = "Real Madrid CF", 
                ShortName = "RMA", 
                Founded = 1902, 
                StadiumId = santiagoBernabeu.Id 
            };
            var atleticoMadrid = new Team 
            { 
                Name = "Atlético Madrid", 
                ShortName = "ATM", 
                Founded = 1903, 
                StadiumId = wandaMetropolitano.Id 
            };
            var sevilla = new Team 
            { 
                Name = "Sevilla FC", 
                ShortName = "SEV", 
                Founded = 1890, 
                StadiumId = ramonSanchezPizjuan.Id 
            };
            var valencia = new Team 
            { 
                Name = "Valencia CF", 
                ShortName = "VAL", 
                Founded = 1919, 
                StadiumId = mestalla.Id 
            };
            
            context.Teams.AddRange(barcelona, realMadrid, atleticoMadrid, sevilla, valencia);
            context.SaveChanges();
            
            var referee1 = new Referee { FirstName = "José María", LastName = "Sánchez Martínez", Nationality = "Spain" };
            var referee2 = new Referee { FirstName = "Antonio Mateu", LastName = "Lahoz", Nationality = "Spain" };
            var referee3 = new Referee { FirstName = "Jesús Gil", LastName = "Manzano", Nationality = "Spain" };
            context.Referees.AddRange(referee1, referee2, referee3);
            context.SaveChanges();
            
            context.Players.AddRange(
                new Player { FirstName = "Robert", LastName = "Lewandowski", Position = "Forward", JerseyNumber = 9, TeamId = barcelona.Id, DateOfBirth = new DateTime(1988, 8, 21) },
                new Player { FirstName = "Pedri", LastName = "González", Position = "Midfielder", JerseyNumber = 8, TeamId = barcelona.Id, DateOfBirth = new DateTime(2002, 11, 25) },
                new Player { FirstName = "Gavi", LastName = "Páez", Position = "Midfielder", JerseyNumber = 6, TeamId = barcelona.Id, DateOfBirth = new DateTime(2004, 8, 5) },
                new Player { FirstName = "Ronald", LastName = "Araújo", Position = "Defender", JerseyNumber = 4, TeamId = barcelona.Id, DateOfBirth = new DateTime(1999, 3, 7) }
            );
            
            context.Players.AddRange(
                new Player { FirstName = "Jude", LastName = "Bellingham", Position = "Midfielder", JerseyNumber = 5, TeamId = realMadrid.Id, DateOfBirth = new DateTime(2003, 6, 29) },
                new Player { FirstName = "Vinícius", LastName = "Júnior", Position = "Forward", JerseyNumber = 7, TeamId = realMadrid.Id, DateOfBirth = new DateTime(2000, 7, 12) },
                new Player { FirstName = "Thibaut", LastName = "Courtois", Position = "Goalkeeper", JerseyNumber = 1, TeamId = realMadrid.Id, DateOfBirth = new DateTime(1992, 5, 11) },
                new Player { FirstName = "Antonio", LastName = "Rüdiger", Position = "Defender", JerseyNumber = 22, TeamId = realMadrid.Id, DateOfBirth = new DateTime(1993, 3, 3) }
            );
            
            context.Players.AddRange(
                new Player { FirstName = "Antoine", LastName = "Griezmann", Position = "Forward", JerseyNumber = 7, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1991, 3, 21) },
                new Player { FirstName = "Jan", LastName = "Oblak", Position = "Goalkeeper", JerseyNumber = 13, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1993, 1, 7) }
            );
            
            context.SaveChanges();
            
            var match1 = new Match
            {
                HomeTeamId = barcelona.Id,
                AwayTeamId = realMadrid.Id,
                MatchDate = new DateTime(2024, 10, 26, 21, 0, 0),
                HomeScore = 2,
                AwayScore = 1,
                IsCompleted = true,
                RefereeId = referee1.Id,
                StadiumId = campNou.Id
            };
            
            var match2 = new Match
            {
                HomeTeamId = atleticoMadrid.Id,
                AwayTeamId = sevilla.Id,
                MatchDate = new DateTime(2026, 05, 21, 18, 30, 0),
                HomeScore = 1,
                AwayScore = 1,
                IsCompleted = true,
                RefereeId = referee2.Id,
                StadiumId = wandaMetropolitano.Id
            };
            
            var match3 = new Match
            {
                HomeTeamId = valencia.Id,
                AwayTeamId = barcelona.Id,
                MatchDate = new DateTime(2026, 05, 22, 16, 15, 0),
                HomeScore = 0,
                AwayScore = 3,
                IsCompleted = true,
                RefereeId = referee3.Id,
                StadiumId = mestalla.Id
            };
            
            var match4 = new Match
            {
                HomeTeamId = realMadrid.Id,
                AwayTeamId = atleticoMadrid.Id,
                MatchDate = new DateTime(2026, 06, 03, 21, 0, 0),
                HomeScore = 0,
                AwayScore = 0,
                IsCompleted = false,
                RefereeId = referee1.Id,
                StadiumId = santiagoBernabeu.Id
            };
            
            context.Matches.AddRange(match1, match2, match3, match4);
            context.SaveChanges();
            
            var lewandowski = context.Players.First(p => p.LastName == "Lewandowski");
            var bellingham = context.Players.First(p => p.LastName == "Bellingham");
            var vinicius = context.Players.First(p => p.LastName == "Júnior");
            var griezmann = context.Players.First(p => p.LastName == "Griezmann");
            
            context.PlayerStats.AddRange(
                new PlayerStats { PlayerId = lewandowski.Id, MatchId = match1.Id, Goals = 2, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = bellingham.Id, MatchId = match1.Id, Goals = 1, Assists = 0, YellowCards = 1, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = vinicius.Id, MatchId = match1.Id, Goals = 0, Assists = 1, YellowCards = 0, RedCards = 0, MinutesPlayed = 85 }
            );
            
            context.PlayerStats.Add(new PlayerStats 
            { 
                PlayerId = lewandowski.Id, 
                MatchId = match3.Id, 
                Goals = 1, 
                Assists = 1, 
                YellowCards = 0, 
                RedCards = 0, 
                MinutesPlayed = 78 
            });
            
            context.SaveChanges();
            
            Console.WriteLine("Database seeded successfully!");
        }
    }
}