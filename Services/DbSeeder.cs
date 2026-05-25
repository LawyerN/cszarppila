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

            // Admin user
            var adminUser = new User
            {
                Username = adminUsername,
                ApiToken = Guid.NewGuid().ToString("N"),
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow
            };

            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, adminPassword);

            context.Users.Add(adminUser);

            // ------------------- Stadiums -------------------
            var stadiums = new[]
            {
                new Stadium { Name = "Spotify Camp Nou", City = "Barcelona", Capacity = 99354 },
                new Stadium { Name = "Santiago Bernabéu", City = "Madrid", Capacity = 81044 },
                new Stadium { Name = "Cívitas Metropolitano", City = "Madrid", Capacity = 68456 },
                new Stadium { Name = "Ramón Sánchez Pizjuán", City = "Sevilla", Capacity = 43883 },
                new Stadium { Name = "Mestalla", City = "Valencia", Capacity = 49430 },
                new Stadium { Name = "Reale Arena", City = "San Sebastián", Capacity = 39313 },
                new Stadium { Name = "Estadio de la Cerámica", City = "Villarreal", Capacity = 23500 },
                new Stadium { Name = "Benito Villamarín", City = "Sevilla", Capacity = 60721 },
                new Stadium { Name = "San Mamés", City = "Bilbao", Capacity = 53289 },
                new Stadium { Name = "El Sadar", City = "Pamplona", Capacity = 23576 },
                new Stadium { Name = "Balaídos", City = "Vigo", Capacity = 29000 },
                new Stadium { Name = "Vallecas", City = "Madrid", Capacity = 14708 },
                new Stadium { Name = "Visit Mallorca Estadi", City = "Palma", Capacity = 23142 },
                new Stadium { Name = "Montilivi", City = "Girona", Capacity = 13500 },
                new Stadium { Name = "Mendizorrotza", City = "Vitoria-Gasteiz", Capacity = 19840 },
                new Stadium { Name = "Gran Canaria", City = "Las Palmas", Capacity = 32400 },
                new Stadium { Name = "Nuevo Los Cármenes", City = "Granada", Capacity = 19336 },
                new Stadium { Name = "Coliseum Alfonso Pérez", City = "Getafe", Capacity = 17393 },
                new Stadium { Name = "Nuevo Mirandilla", City = "Cádiz", Capacity = 20724 },
                new Stadium { Name = "Power Horse Stadium", City = "Almería", Capacity = 15274 }
            };
            context.Stadiums.AddRange(stadiums);
            context.SaveChanges();

            // ------------------- Teams -------------------
            var barcelona = new Team { Name = "FC Barcelona", ShortName = "BAR", Founded = 1899, StadiumId = stadiums[0].Id };
            var realMadrid = new Team { Name = "Real Madrid CF", ShortName = "RMA", Founded = 1902, StadiumId = stadiums[1].Id };
            var atleticoMadrid = new Team { Name = "Atlético Madrid", ShortName = "ATM", Founded = 1903, StadiumId = stadiums[2].Id };
            var sevilla = new Team { Name = "Sevilla FC", ShortName = "SEV", Founded = 1890, StadiumId = stadiums[3].Id };
            var valencia = new Team { Name = "Valencia CF", ShortName = "VAL", Founded = 1919, StadiumId = stadiums[4].Id };
            var realSociedad = new Team { Name = "Real Sociedad", ShortName = "RSO", Founded = 1909, StadiumId = stadiums[5].Id };
            var villarreal = new Team { Name = "Villarreal CF", ShortName = "VIL", Founded = 1923, StadiumId = stadiums[6].Id };
            var betis = new Team { Name = "Real Betis", ShortName = "BET", Founded = 1907, StadiumId = stadiums[7].Id };
            var athletic = new Team { Name = "Athletic Club", ShortName = "ATH", Founded = 1898, StadiumId = stadiums[8].Id };
            var osasuna = new Team { Name = "CA Osasuna", ShortName = "OSA", Founded = 1920, StadiumId = stadiums[9].Id };
            var celta = new Team { Name = "Celta de Vigo", ShortName = "CEL", Founded = 1923, StadiumId = stadiums[10].Id };
            var rayo = new Team { Name = "Rayo Vallecano", ShortName = "RAY", Founded = 1924, StadiumId = stadiums[11].Id };
            var mallorca = new Team { Name = "RCD Mallorca", ShortName = "MAL", Founded = 1916, StadiumId = stadiums[12].Id };
            var girona = new Team { Name = "Girona FC", ShortName = "GIR", Founded = 1930, StadiumId = stadiums[13].Id };
            var alaves = new Team { Name = "Deportivo Alavés", ShortName = "ALA", Founded = 1921, StadiumId = stadiums[14].Id };
            var lasPalmas = new Team { Name = "UD Las Palmas", ShortName = "LPA", Founded = 1949, StadiumId = stadiums[15].Id };
            var granada = new Team { Name = "Granada CF", ShortName = "GRA", Founded = 1931, StadiumId = stadiums[16].Id };
            var getafe = new Team { Name = "Getafe CF", ShortName = "GET", Founded = 1983, StadiumId = stadiums[17].Id };
            var cadiz = new Team { Name = "Cádiz CF", ShortName = "CAD", Founded = 1910, StadiumId = stadiums[18].Id };
            var almeria = new Team { Name = "UD Almería", ShortName = "ALM", Founded = 1989, StadiumId = stadiums[19].Id };

            var teams = new[] { barcelona, realMadrid, atleticoMadrid, sevilla, valencia, realSociedad, villarreal, betis, athletic, osasuna, celta, rayo, mallorca, girona, alaves, lasPalmas, granada, getafe, cadiz, almeria };
            context.Teams.AddRange(teams);
            context.SaveChanges();

            // ------------------- Referees -------------------
            var referees = new[]
            {
                new Referee { FirstName = "José María", LastName = "Sánchez Martínez", Nationality = "Spain" },
                new Referee { FirstName = "Antonio Mateu", LastName = "Lahoz", Nationality = "Spain" },
                new Referee { FirstName = "Jesús Gil", LastName = "Manzano", Nationality = "Spain" },
                new Referee { FirstName = "Carlos del Cerro", LastName = "Grande", Nationality = "Spain" },
                new Referee { FirstName = "Alejandro José", LastName = "Hernández Hernández", Nationality = "Spain" }
            };
            context.Referees.AddRange(referees);
            context.SaveChanges();

            // ------------------- Players (6 per team) -------------------
            var players = new[]
            {
                // Barcelona
                new Player { FirstName = "Robert", LastName = "Lewandowski", Position = "Forward", JerseyNumber = 9, TeamId = barcelona.Id, DateOfBirth = new DateTime(1988, 8, 21) },
                new Player { FirstName = "Pedri", LastName = "González", Position = "Midfielder", JerseyNumber = 8, TeamId = barcelona.Id, DateOfBirth = new DateTime(2002, 11, 25) },
                new Player { FirstName = "Gavi", LastName = "Páez", Position = "Midfielder", JerseyNumber = 6, TeamId = barcelona.Id, DateOfBirth = new DateTime(2004, 8, 5) },
                new Player { FirstName = "Ronald", LastName = "Araújo", Position = "Defender", JerseyNumber = 4, TeamId = barcelona.Id, DateOfBirth = new DateTime(1999, 3, 7) },
                new Player { FirstName = "Frenkie", LastName = "de Jong", Position = "Midfielder", JerseyNumber = 21, TeamId = barcelona.Id, DateOfBirth = new DateTime(1997, 5, 12) },
                new Player { FirstName = "Lamine", LastName = "Yamal", Position = "Forward", JerseyNumber = 27, TeamId = barcelona.Id, DateOfBirth = new DateTime(2007, 7, 13) },
                // Real Madrid
                new Player { FirstName = "Jude", LastName = "Bellingham", Position = "Midfielder", JerseyNumber = 5, TeamId = realMadrid.Id, DateOfBirth = new DateTime(2003, 6, 29) },
                new Player { FirstName = "Vinícius", LastName = "Júnior", Position = "Forward", JerseyNumber = 7, TeamId = realMadrid.Id, DateOfBirth = new DateTime(2000, 7, 12) },
                new Player { FirstName = "Thibaut", LastName = "Courtois", Position = "Goalkeeper", JerseyNumber = 1, TeamId = realMadrid.Id, DateOfBirth = new DateTime(1992, 5, 11) },
                new Player { FirstName = "Antonio", LastName = "Rüdiger", Position = "Defender", JerseyNumber = 22, TeamId = realMadrid.Id, DateOfBirth = new DateTime(1993, 3, 3) },
                new Player { FirstName = "Rodrygo", LastName = "Goes", Position = "Forward", JerseyNumber = 11, TeamId = realMadrid.Id, DateOfBirth = new DateTime(2001, 1, 9) },
                new Player { FirstName = "Federico", LastName = "Valverde", Position = "Midfielder", JerseyNumber = 15, TeamId = realMadrid.Id, DateOfBirth = new DateTime(1998, 7, 22) },
                // Atlético Madrid
                new Player { FirstName = "Antoine", LastName = "Griezmann", Position = "Forward", JerseyNumber = 7, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1991, 3, 21) },
                new Player { FirstName = "Jan", LastName = "Oblak", Position = "Goalkeeper", JerseyNumber = 13, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1993, 1, 7) },
                new Player { FirstName = "Marcos", LastName = "Llorente", Position = "Midfielder", JerseyNumber = 14, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1995, 1, 30) },
                new Player { FirstName = "José María", LastName = "Giménez", Position = "Defender", JerseyNumber = 2, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1995, 1, 20) },
                new Player { FirstName = "Álvaro", LastName = "Morata", Position = "Forward", JerseyNumber = 19, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1992, 10, 23) },
                new Player { FirstName = "Rodrigo", LastName = "De Paul", Position = "Midfielder", JerseyNumber = 5, TeamId = atleticoMadrid.Id, DateOfBirth = new DateTime(1994, 5, 24) },
                // Sevilla
                new Player { FirstName = "Youssef", LastName = "En-Nesyri", Position = "Forward", JerseyNumber = 15, TeamId = sevilla.Id, DateOfBirth = new DateTime(1997, 6, 1) },
                new Player { FirstName = "Lucas", LastName = "Ocampos", Position = "Forward", JerseyNumber = 5, TeamId = sevilla.Id, DateOfBirth = new DateTime(1994, 7, 11) },
                new Player { FirstName = "Jesús", LastName = "Navas", Position = "Defender", JerseyNumber = 16, TeamId = sevilla.Id, DateOfBirth = new DateTime(1985, 11, 21) },
                new Player { FirstName = "Ivan", LastName = "Rakitić", Position = "Midfielder", JerseyNumber = 10, TeamId = sevilla.Id, DateOfBirth = new DateTime(1988, 3, 10) },
                new Player { FirstName = "Marcos", LastName = "Acuña", Position = "Defender", JerseyNumber = 19, TeamId = sevilla.Id, DateOfBirth = new DateTime(1991, 10, 28) },
                new Player { FirstName = "Bono", LastName = "", Position = "Goalkeeper", JerseyNumber = 13, TeamId = sevilla.Id, DateOfBirth = new DateTime(1991, 4, 5) },
                // Valencia
                new Player { FirstName = "Giorgi", LastName = "Mamardashvili", Position = "Goalkeeper", JerseyNumber = 25, TeamId = valencia.Id, DateOfBirth = new DateTime(2000, 9, 29) },
                new Player { FirstName = "José", LastName = "Gayà", Position = "Defender", JerseyNumber = 14, TeamId = valencia.Id, DateOfBirth = new DateTime(1995, 5, 25) },
                new Player { FirstName = "Yunus", LastName = "Musah", Position = "Midfielder", JerseyNumber = 6, TeamId = valencia.Id, DateOfBirth = new DateTime(2002, 11, 29) },
                new Player { FirstName = "Hugo", LastName = "Duro", Position = "Forward", JerseyNumber = 19, TeamId = valencia.Id, DateOfBirth = new DateTime(1999, 11, 10) },
                new Player { FirstName = "André", LastName = "Almeida", Position = "Midfielder", JerseyNumber = 10, TeamId = valencia.Id, DateOfBirth = new DateTime(2000, 5, 30) },
                new Player { FirstName = "Thierry", LastName = "Correia", Position = "Defender", JerseyNumber = 2, TeamId = valencia.Id, DateOfBirth = new DateTime(1999, 3, 9) },
                // Real Sociedad
                new Player { FirstName = "Mikel", LastName = "Oyarzabal", Position = "Forward", JerseyNumber = 10, TeamId = realSociedad.Id, DateOfBirth = new DateTime(1997, 4, 21) },
                new Player { FirstName = "Takefusa", LastName = "Kubo", Position = "Forward", JerseyNumber = 14, TeamId = realSociedad.Id, DateOfBirth = new DateTime(2001, 6, 4) },
                new Player { FirstName = "Martín", LastName = "Zubimendi", Position = "Midfielder", JerseyNumber = 4, TeamId = realSociedad.Id, DateOfBirth = new DateTime(1999, 2, 2) },
                new Player { FirstName = "Robin", LastName = "Le Normand", Position = "Defender", JerseyNumber = 24, TeamId = realSociedad.Id, DateOfBirth = new DateTime(1996, 11, 11) },
                new Player { FirstName = "Álex", LastName = "Remiro", Position = "Goalkeeper", JerseyNumber = 1, TeamId = realSociedad.Id, DateOfBirth = new DateTime(1995, 3, 24) },
                new Player { FirstName = "Brais", LastName = "Méndez", Position = "Midfielder", JerseyNumber = 8, TeamId = realSociedad.Id, DateOfBirth = new DateTime(1997, 1, 1) },
                // Villarreal
                new Player { FirstName = "Gerard", LastName = "Moreno", Position = "Forward", JerseyNumber = 7, TeamId = villarreal.Id, DateOfBirth = new DateTime(1992, 4, 7) },
                new Player { FirstName = "Álex", LastName = "Baena", Position = "Midfielder", JerseyNumber = 16, TeamId = villarreal.Id, DateOfBirth = new DateTime(2001, 7, 20) },
                new Player { FirstName = "Juan", LastName = "Foyth", Position = "Defender", JerseyNumber = 8, TeamId = villarreal.Id, DateOfBirth = new DateTime(1998, 1, 12) },
                new Player { FirstName = "Filip", LastName = "Jörgensen", Position = "Goalkeeper", JerseyNumber = 13, TeamId = villarreal.Id, DateOfBirth = new DateTime(2002, 4, 22) },
                new Player { FirstName = "Etienne", LastName = "Capoue", Position = "Midfielder", JerseyNumber = 6, TeamId = villarreal.Id, DateOfBirth = new DateTime(1988, 7, 11) },
                new Player { FirstName = "Alfonso", LastName = "Pedraza", Position = "Defender", JerseyNumber = 24, TeamId = villarreal.Id, DateOfBirth = new DateTime(1996, 4, 9) },
                // Real Betis
                new Player { FirstName = "Isco", LastName = "Alarcón", Position = "Midfielder", JerseyNumber = 22, TeamId = betis.Id, DateOfBirth = new DateTime(1992, 4, 21) },
                new Player { FirstName = "Borja", LastName = "Iglesias", Position = "Forward", JerseyNumber = 9, TeamId = betis.Id, DateOfBirth = new DateTime(1993, 1, 17) },
                new Player { FirstName = "Nabil", LastName = "Fekir", Position = "Midfielder", JerseyNumber = 8, TeamId = betis.Id, DateOfBirth = new DateTime(1993, 7, 18) },
                new Player { FirstName = "Aitor", LastName = "Ruibal", Position = "Defender", JerseyNumber = 24, TeamId = betis.Id, DateOfBirth = new DateTime(1996, 3, 22) },
                new Player { FirstName = "Rui", LastName = "Silva", Position = "Goalkeeper", JerseyNumber = 13, TeamId = betis.Id, DateOfBirth = new DateTime(1994, 2, 7) },
                new Player { FirstName = "Guido", LastName = "Rodríguez", Position = "Midfielder", JerseyNumber = 5, TeamId = betis.Id, DateOfBirth = new DateTime(1994, 4, 12) },
                // Athletic Club
                new Player { FirstName = "Iñaki", LastName = "Williams", Position = "Forward", JerseyNumber = 9, TeamId = athletic.Id, DateOfBirth = new DateTime(1994, 6, 15) },
                new Player { FirstName = "Nico", LastName = "Williams", Position = "Forward", JerseyNumber = 11, TeamId = athletic.Id, DateOfBirth = new DateTime(2002, 7, 12) },
                new Player { FirstName = "Iker", LastName = "Muniain", Position = "Midfielder", JerseyNumber = 10, TeamId = athletic.Id, DateOfBirth = new DateTime(1992, 12, 19) },
                new Player { FirstName = "Unai", LastName = "Simón", Position = "Goalkeeper", JerseyNumber = 1, TeamId = athletic.Id, DateOfBirth = new DateTime(1997, 6, 11) },
                new Player { FirstName = "Yeray", LastName = "Álvarez", Position = "Defender", JerseyNumber = 5, TeamId = athletic.Id, DateOfBirth = new DateTime(1995, 1, 24) },
                new Player { FirstName = "Oihan", LastName = "Sancet", Position = "Midfielder", JerseyNumber = 8, TeamId = athletic.Id, DateOfBirth = new DateTime(2000, 4, 25) },
                // Osasuna
                new Player { FirstName = "Ante", LastName = "Budimir", Position = "Forward", JerseyNumber = 17, TeamId = osasuna.Id, DateOfBirth = new DateTime(1991, 7, 22) },
                new Player { FirstName = "Aimar", LastName = "Oroz", Position = "Midfielder", JerseyNumber = 10, TeamId = osasuna.Id, DateOfBirth = new DateTime(2001, 11, 27) },
                new Player { FirstName = "Moi", LastName = "Gómez", Position = "Midfielder", JerseyNumber = 16, TeamId = osasuna.Id, DateOfBirth = new DateTime(1994, 6, 23) },
                new Player { FirstName = "David", LastName = "García", Position = "Defender", JerseyNumber = 5, TeamId = osasuna.Id, DateOfBirth = new DateTime(1994, 2, 14) },
                new Player { FirstName = "Sergio", LastName = "Herrera", Position = "Goalkeeper", JerseyNumber = 1, TeamId = osasuna.Id, DateOfBirth = new DateTime(1993, 6, 5) },
                new Player { FirstName = "Rubén", LastName = "Peña", Position = "Defender", JerseyNumber = 15, TeamId = osasuna.Id, DateOfBirth = new DateTime(1991, 7, 11) },
                // Celta
                new Player { FirstName = "Iago", LastName = "Aspas", Position = "Forward", JerseyNumber = 10, TeamId = celta.Id, DateOfBirth = new DateTime(1987, 8, 1) },
                new Player { FirstName = "Jörgen Strand", LastName = "Larsen", Position = "Forward", JerseyNumber = 18, TeamId = celta.Id, DateOfBirth = new DateTime(2000, 2, 6) },
                new Player { FirstName = "Fran", LastName = "Beltrán", Position = "Midfielder", JerseyNumber = 8, TeamId = celta.Id, DateOfBirth = new DateTime(1999, 3, 3) },
                new Player { FirstName = "Unai", LastName = "Núñez", Position = "Defender", JerseyNumber = 4, TeamId = celta.Id, DateOfBirth = new DateTime(1997, 1, 30) },
                new Player { FirstName = "Iván", LastName = "Villar", Position = "Goalkeeper", JerseyNumber = 1, TeamId = celta.Id, DateOfBirth = new DateTime(1997, 7, 9) },
                new Player { FirstName = "Óscar", LastName = "Mingueza", Position = "Defender", JerseyNumber = 3, TeamId = celta.Id, DateOfBirth = new DateTime(1999, 5, 13) },
                // Rayo
                new Player { FirstName = "Isi", LastName = "Palazón", Position = "Forward", JerseyNumber = 7, TeamId = rayo.Id, DateOfBirth = new DateTime(1994, 12, 27) },
                new Player { FirstName = "Raúl", LastName = "de Tomás", Position = "Forward", JerseyNumber = 22, TeamId = rayo.Id, DateOfBirth = new DateTime(1994, 10, 17) },
                new Player { FirstName = "Óscar", LastName = "Valentín", Position = "Midfielder", JerseyNumber = 23, TeamId = rayo.Id, DateOfBirth = new DateTime(1994, 8, 20) },
                new Player { FirstName = "Florian", LastName = "Lejeune", Position = "Defender", JerseyNumber = 24, TeamId = rayo.Id, DateOfBirth = new DateTime(1991, 5, 20) },
                new Player { FirstName = "Stole", LastName = "Dimitrievski", Position = "Goalkeeper", JerseyNumber = 1, TeamId = rayo.Id, DateOfBirth = new DateTime(1993, 12, 25) },
                new Player { FirstName = "Álvaro", LastName = "García", Position = "Midfielder", JerseyNumber = 18, TeamId = rayo.Id, DateOfBirth = new DateTime(1992, 6, 1) },
                // Mallorca
                new Player { FirstName = "Vedat", LastName = "Muriqi", Position = "Forward", JerseyNumber = 7, TeamId = mallorca.Id, DateOfBirth = new DateTime(1994, 4, 24) },
                new Player { FirstName = "Kang-in", LastName = "Lee", Position = "Midfielder", JerseyNumber = 19, TeamId = mallorca.Id, DateOfBirth = new DateTime(2001, 2, 19) },
                new Player { FirstName = "Dani", LastName = "Rodríguez", Position = "Midfielder", JerseyNumber = 14, TeamId = mallorca.Id, DateOfBirth = new DateTime(1988, 6, 6) },
                new Player { FirstName = "Antonio", LastName = "Raíllo", Position = "Defender", JerseyNumber = 21, TeamId = mallorca.Id, DateOfBirth = new DateTime(1991, 10, 8) },
                new Player { FirstName = "Predrag", LastName = "Rajković", Position = "Goalkeeper", JerseyNumber = 1, TeamId = mallorca.Id, DateOfBirth = new DateTime(1995, 10, 31) },
                new Player { FirstName = "Pablo", LastName = "Maffeo", Position = "Defender", JerseyNumber = 15, TeamId = mallorca.Id, DateOfBirth = new DateTime(1997, 7, 12) },
                // Girona
                new Player { FirstName = "Artem", LastName = "Dovbyk", Position = "Forward", JerseyNumber = 9, TeamId = girona.Id, DateOfBirth = new DateTime(1997, 6, 21) },
                new Player { FirstName = "Viktor", LastName = "Tsygankov", Position = "Forward", JerseyNumber = 8, TeamId = girona.Id, DateOfBirth = new DateTime(1997, 11, 15) },
                new Player { FirstName = "Aleix", LastName = "García", Position = "Midfielder", JerseyNumber = 14, TeamId = girona.Id, DateOfBirth = new DateTime(1997, 6, 28) },
                new Player { FirstName = "Daley", LastName = "Blind", Position = "Defender", JerseyNumber = 17, TeamId = girona.Id, DateOfBirth = new DateTime(1990, 3, 9) },
                new Player { FirstName = "Paulo", LastName = "Gazzaniga", Position = "Goalkeeper", JerseyNumber = 13, TeamId = girona.Id, DateOfBirth = new DateTime(1992, 1, 2) },
                new Player { FirstName = "Miguel", LastName = "Gutiérrez", Position = "Defender", JerseyNumber = 3, TeamId = girona.Id, DateOfBirth = new DateTime(2001, 7, 27) },
                // Alavés
                new Player { FirstName = "Samu", LastName = "Omorodion", Position = "Forward", JerseyNumber = 32, TeamId = alaves.Id, DateOfBirth = new DateTime(2004, 5, 5) },
                new Player { FirstName = "Luis", LastName = "Rioja", Position = "Forward", JerseyNumber = 11, TeamId = alaves.Id, DateOfBirth = new DateTime(1993, 10, 16) },
                new Player { FirstName = "Jon", LastName = "Guridi", Position = "Midfielder", JerseyNumber = 18, TeamId = alaves.Id, DateOfBirth = new DateTime(1995, 2, 28) },
                new Player { FirstName = "Aleksandar", LastName = "Sedlar", Position = "Defender", JerseyNumber = 4, TeamId = alaves.Id, DateOfBirth = new DateTime(1991, 12, 13) },
                new Player { FirstName = "Antonio", LastName = "Sivera", Position = "Goalkeeper", JerseyNumber = 1, TeamId = alaves.Id, DateOfBirth = new DateTime(1996, 8, 11) },
                new Player { FirstName = "Rafa", LastName = "Marín", Position = "Defender", JerseyNumber = 16, TeamId = alaves.Id, DateOfBirth = new DateTime(2002, 5, 19) },
                // Las Palmas
                new Player { FirstName = "Mika", LastName = "Mármol", Position = "Defender", JerseyNumber = 15, TeamId = lasPalmas.Id, DateOfBirth = new DateTime(2001, 7, 22) },
                new Player { FirstName = "Kirian", LastName = "Rodríguez", Position = "Midfielder", JerseyNumber = 20, TeamId = lasPalmas.Id, DateOfBirth = new DateTime(1996, 3, 5) },
                new Player { FirstName = "Munir", LastName = "El Haddadi", Position = "Forward", JerseyNumber = 17, TeamId = lasPalmas.Id, DateOfBirth = new DateTime(1995, 9, 1) },
                new Player { FirstName = "Álvaro", LastName = "Valles", Position = "Goalkeeper", JerseyNumber = 13, TeamId = lasPalmas.Id, DateOfBirth = new DateTime(1997, 7, 25) },
                new Player { FirstName = "Saúl", LastName = "Coco", Position = "Defender", JerseyNumber = 6, TeamId = lasPalmas.Id, DateOfBirth = new DateTime(2001, 2, 3) },
                new Player { FirstName = "Javi", LastName = "Muñoz", Position = "Midfielder", JerseyNumber = 5, TeamId = lasPalmas.Id, DateOfBirth = new DateTime(1995, 2, 25) },
                // Granada
                new Player { FirstName = "Myrto", LastName = "Uzuni", Position = "Forward", JerseyNumber = 11, TeamId = granada.Id, DateOfBirth = new DateTime(1995, 5, 31) },
                new Player { FirstName = "Lucas", LastName = "Boyé", Position = "Forward", JerseyNumber = 7, TeamId = granada.Id, DateOfBirth = new DateTime(1996, 2, 28) },
                new Player { FirstName = "Gonzalo", LastName = "Villar", Position = "Midfielder", JerseyNumber = 24, TeamId = granada.Id, DateOfBirth = new DateTime(1998, 3, 23) },
                new Player { FirstName = "Ignasi", LastName = "Miquel", Position = "Defender", JerseyNumber = 14, TeamId = granada.Id, DateOfBirth = new DateTime(1992, 9, 28) },
                new Player { FirstName = "Raúl", LastName = "Fernández", Position = "Goalkeeper", JerseyNumber = 1, TeamId = granada.Id, DateOfBirth = new DateTime(1988, 3, 13) },
                new Player { FirstName = "Carlos", LastName = "Neva", Position = "Defender", JerseyNumber = 3, TeamId = granada.Id, DateOfBirth = new DateTime(1996, 6, 12) },
                // Getafe
                new Player { FirstName = "Borja", LastName = "Mayoral", Position = "Forward", JerseyNumber = 19, TeamId = getafe.Id, DateOfBirth = new DateTime(1997, 4, 5) },
                new Player { FirstName = "Enes", LastName = "Ünal", Position = "Forward", JerseyNumber = 10, TeamId = getafe.Id, DateOfBirth = new DateTime(1997, 5, 10) },
                new Player { FirstName = "Nemanja", LastName = "Maksimović", Position = "Midfielder", JerseyNumber = 20, TeamId = getafe.Id, DateOfBirth = new DateTime(1995, 1, 26) },
                new Player { FirstName = "Djené", LastName = "Dakonam", Position = "Defender", JerseyNumber = 2, TeamId = getafe.Id, DateOfBirth = new DateTime(1991, 12, 31) },
                new Player { FirstName = "David", LastName = "Soria", Position = "Goalkeeper", JerseyNumber = 13, TeamId = getafe.Id, DateOfBirth = new DateTime(1993, 4, 4) },
                new Player { FirstName = "Carles", LastName = "Aleñá", Position = "Midfielder", JerseyNumber = 11, TeamId = getafe.Id, DateOfBirth = new DateTime(1998, 1, 5) },
                // Cádiz
                new Player { FirstName = "Chris", LastName = "Ramos", Position = "Forward", JerseyNumber = 16, TeamId = cadiz.Id, DateOfBirth = new DateTime(1997, 1, 18) },
                new Player { FirstName = "Roger", LastName = "Martí", Position = "Forward", JerseyNumber = 21, TeamId = cadiz.Id, DateOfBirth = new DateTime(1991, 1, 24) },
                new Player { FirstName = "Álex", LastName = "Fernández", Position = "Midfielder", JerseyNumber = 8, TeamId = cadiz.Id, DateOfBirth = new DateTime(1992, 10, 15) },
                new Player { FirstName = "Fali", LastName = "Giménez", Position = "Defender", JerseyNumber = 3, TeamId = cadiz.Id, DateOfBirth = new DateTime(1993, 2, 25) },
                new Player { FirstName = "Jeremías", LastName = "Ledesma", Position = "Goalkeeper", JerseyNumber = 1, TeamId = cadiz.Id, DateOfBirth = new DateTime(1993, 2, 13) },
                new Player { FirstName = "Iza", LastName = "Carcelén", Position = "Defender", JerseyNumber = 20, TeamId = cadiz.Id, DateOfBirth = new DateTime(1993, 4, 23) },
                // Almería
                new Player { FirstName = "Luis", LastName = "Suárez", Position = "Forward", JerseyNumber = 9, TeamId = almeria.Id, DateOfBirth = new DateTime(1997, 12, 2) },
                new Player { FirstName = "Sergio", LastName = "Arribas", Position = "Midfielder", JerseyNumber = 19, TeamId = almeria.Id, DateOfBirth = new DateTime(2001, 9, 30) },
                new Player { FirstName = "Largie", LastName = "Ramazani", Position = "Forward", JerseyNumber = 7, TeamId = almeria.Id, DateOfBirth = new DateTime(2001, 2, 27) },
                new Player { FirstName = "César", LastName = "Montes", Position = "Defender", JerseyNumber = 22, TeamId = almeria.Id, DateOfBirth = new DateTime(1997, 2, 24) },
                new Player { FirstName = "Luis", LastName = "Maximiano", Position = "Goalkeeper", JerseyNumber = 1, TeamId = almeria.Id, DateOfBirth = new DateTime(1999, 1, 5) },
                new Player { FirstName = "Edgar", LastName = "González", Position = "Defender", JerseyNumber = 3, TeamId = almeria.Id, DateOfBirth = new DateTime(1997, 4, 1) }
            };

            context.Players.AddRange(players);
            context.SaveChanges();

            // ------------------- Matches -------------------
            // El Clásico (completed)
            var match1 = new Match
            {
                HomeTeamId = barcelona.Id,
                AwayTeamId = realMadrid.Id,
                MatchDate = new DateTime(2024, 10, 26, 21, 0, 0),
                HomeScore = 2,
                AwayScore = 1,
                IsCompleted = true,
                RefereeId = referees[0].Id,
                StadiumId = stadiums[0].Id
            };
            // Atlético vs Sevilla (completed)
            var match2 = new Match
            {
                HomeTeamId = atleticoMadrid.Id,
                AwayTeamId = sevilla.Id,
                MatchDate = new DateTime(2026, 05, 21, 18, 30, 0),
                HomeScore = 1,
                AwayScore = 1,
                IsCompleted = true,
                RefereeId = referees[1].Id,
                StadiumId = stadiums[2].Id
            };
            // Valencia vs Barcelona (completed)
            var match3 = new Match
            {
                HomeTeamId = valencia.Id,
                AwayTeamId = barcelona.Id,
                MatchDate = new DateTime(2026, 05, 22, 16, 15, 0),
                HomeScore = 0,
                AwayScore = 3,
                IsCompleted = true,
                RefereeId = referees[2].Id,
                StadiumId = stadiums[4].Id
            };
            // Real Madrid vs Atlético (upcoming)
            var match4 = new Match
            {
                HomeTeamId = realMadrid.Id,
                AwayTeamId = atleticoMadrid.Id,
                MatchDate = new DateTime(2026, 06, 03, 21, 0, 0),
                HomeScore = 0,
                AwayScore = 0,
                IsCompleted = false,
                RefereeId = referees[0].Id,
                StadiumId = stadiums[1].Id
            };
            // Real Sociedad vs Athletic (Basque derby, completed)
            var match5 = new Match
            {
                HomeTeamId = realSociedad.Id,
                AwayTeamId = athletic.Id,
                MatchDate = new DateTime(2025, 12, 15, 20, 0, 0),
                HomeScore = 3,
                AwayScore = 2,
                IsCompleted = true,
                RefereeId = referees[3].Id,
                StadiumId = stadiums[5].Id
            };
            // Villarreal vs Betis (completed)
            var match6 = new Match
            {
                HomeTeamId = villarreal.Id,
                AwayTeamId = betis.Id,
                MatchDate = new DateTime(2026, 01, 08, 18, 30, 0),
                HomeScore = 2,
                AwayScore = 2,
                IsCompleted = true,
                RefereeId = referees[4].Id,
                StadiumId = stadiums[6].Id
            };
            // Osasuna vs Celta (completed)
            var match7 = new Match
            {
                HomeTeamId = osasuna.Id,
                AwayTeamId = celta.Id,
                MatchDate = new DateTime(2026, 02, 14, 16, 15, 0),
                HomeScore = 1,
                AwayScore = 0,
                IsCompleted = true,
                RefereeId = referees[1].Id,
                StadiumId = stadiums[9].Id
            };
            // Rayo vs Mallorca (upcoming)
            var match8 = new Match
            {
                HomeTeamId = rayo.Id,
                AwayTeamId = mallorca.Id,
                MatchDate = new DateTime(2026, 06, 07, 17, 0, 0),
                HomeScore = 0,
                AwayScore = 0,
                IsCompleted = false,
                RefereeId = referees[2].Id,
                StadiumId = stadiums[11].Id
            };
            // Girona vs Alavés (completed)
            var match9 = new Match
            {
                HomeTeamId = girona.Id,
                AwayTeamId = alaves.Id,
                MatchDate = new DateTime(2026, 03, 04, 20, 30, 0),
                HomeScore = 4,
                AwayScore = 1,
                IsCompleted = true,
                RefereeId = referees[0].Id,
                StadiumId = stadiums[13].Id
            };
            // Las Palmas vs Granada (completed)
            var match10 = new Match
            {
                HomeTeamId = lasPalmas.Id,
                AwayTeamId = granada.Id,
                MatchDate = new DateTime(2026, 03, 19, 21, 0, 0),
                HomeScore = 2,
                AwayScore = 0,
                IsCompleted = true,
                RefereeId = referees[3].Id,
                StadiumId = stadiums[15].Id
            };
            // Getafe vs Cádiz (upcoming)
            var match11 = new Match
            {
                HomeTeamId = getafe.Id,
                AwayTeamId = cadiz.Id,
                MatchDate = new DateTime(2026, 06, 14, 18, 30, 0),
                HomeScore = 0,
                AwayScore = 0,
                IsCompleted = false,
                RefereeId = referees[4].Id,
                StadiumId = stadiums[17].Id
            };
            // Almería vs Real Madrid (completed)
            var match12 = new Match
            {
                HomeTeamId = almeria.Id,
                AwayTeamId = realMadrid.Id,
                MatchDate = new DateTime(2026, 04, 22, 20, 0, 0),
                HomeScore = 1,
                AwayScore = 3,
                IsCompleted = true,
                RefereeId = referees[1].Id,
                StadiumId = stadiums[19].Id
            };

            context.Matches.AddRange(match1, match2, match3, match4, match5, match6, match7, match8, match9, match10, match11, match12);
            context.SaveChanges();

            // ------------------- Player Stats -------------------
            var lewandowski = context.Players.First(p => p.LastName == "Lewandowski");
            var bellingham = context.Players.First(p => p.LastName == "Bellingham");
            var vinicius = context.Players.First(p => p.LastName == "Júnior");
            var griezmann = context.Players.First(p => p.LastName == "Griezmann");
            var oyarzabal = context.Players.First(p => p.LastName == "Oyarzabal");
            var inaki = context.Players.First(p => p.LastName == "Williams" && p.FirstName == "Iñaki");
            var nico = context.Players.First(p => p.LastName == "Williams" && p.FirstName == "Nico");
            var gerard = context.Players.First(p => p.LastName == "Moreno");
            var isco = context.Players.First(p => p.LastName == "Alarcón");
            var budimir = context.Players.First(p => p.LastName == "Budimir");
            var dovbyk = context.Players.First(p => p.LastName == "Dovbyk");
            var samu = context.Players.First(p => p.LastName == "Omorodion");
            var bellinghamMadrid = bellingham; // already Real Madrid
            var suarezAlmeria = context.Players.First(p => p.LastName == "Suárez" && p.TeamId == almeria.Id);
            var rodrygo = context.Players.First(p => p.LastName == "Goes");
            var valverde = context.Players.First(p => p.LastName == "Valverde");

            // Match1: Barcelona 2-1 Real Madrid
            context.PlayerStats.AddRange(
                new PlayerStats { PlayerId = lewandowski.Id, MatchId = match1.Id, Goals = 2, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = bellingham.Id, MatchId = match1.Id, Goals = 1, Assists = 0, YellowCards = 1, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = vinicius.Id, MatchId = match1.Id, Goals = 0, Assists = 1, YellowCards = 0, RedCards = 0, MinutesPlayed = 85 }
            );

            // Match3: Valencia 0-3 Barcelona
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

            // Match5: Real Sociedad 3-2 Athletic
            context.PlayerStats.AddRange(
                new PlayerStats { PlayerId = oyarzabal.Id, MatchId = match5.Id, Goals = 2, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = inaki.Id, MatchId = match5.Id, Goals = 1, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = nico.Id, MatchId = match5.Id, Goals = 1, Assists = 1, YellowCards = 1, RedCards = 0, MinutesPlayed = 82 }
            );

            // Match6: Villarreal 2-2 Betis
            context.PlayerStats.AddRange(
                new PlayerStats { PlayerId = gerard.Id, MatchId = match6.Id, Goals = 1, Assists = 1, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = isco.Id, MatchId = match6.Id, Goals = 1, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 }
            );

            // Match7: Osasuna 1-0 Celta
            context.PlayerStats.Add(new PlayerStats { PlayerId = budimir.Id, MatchId = match7.Id, Goals = 1, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 85 });

            // Match9: Girona 4-1 Alavés
            context.PlayerStats.AddRange(
                new PlayerStats { PlayerId = dovbyk.Id, MatchId = match9.Id, Goals = 2, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = samu.Id, MatchId = match9.Id, Goals = 1, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 75 }
            );

            // Match12: Almería 1-3 Real Madrid
            context.PlayerStats.AddRange(
                new PlayerStats { PlayerId = suarezAlmeria.Id, MatchId = match12.Id, Goals = 1, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = bellinghamMadrid.Id, MatchId = match12.Id, Goals = 1, Assists = 1, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
                new PlayerStats { PlayerId = rodrygo.Id, MatchId = match12.Id, Goals = 1, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 80 },
                new PlayerStats { PlayerId = valverde.Id, MatchId = match12.Id, Goals = 1, Assists = 0, YellowCards = 0, RedCards = 0, MinutesPlayed = 90 }
            );

            context.SaveChanges();

            Console.WriteLine("Database seeded successfully!");
        }
    }
}