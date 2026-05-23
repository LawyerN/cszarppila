using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Stadium)
                .WithMany(s => s.Teams)
                .HasForeignKey(t => t.StadiumId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Referee)
                .WithMany()
                .HasForeignKey(m => m.RefereeId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<PlayerStats>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStats)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<PlayerStats>()
                .HasOne(ps => ps.Match)
                .WithMany(m => m.PlayerStats)
                .HasForeignKey(ps => ps.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
                
            modelBuilder.Entity<User>()
                .HasIndex(u => u.ApiToken)
                .IsUnique();
        }
    }
}