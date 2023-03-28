using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Common;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {

        // use it when developing the application
        //when we test the application localy on our pc

        public FootballBettingContext()
        {

        }

        // used bu judje
        // loading of the DbContext with dependancy injection => in real app it is useful

        public FootballBettingContext(DbContextOptions options)
            :base(options)
        {
           
        }

        //DB SETS


        public DbSet<Team> Teams { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerStatistic> PlayersStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }








        // Connection Configurating

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Set default connection string
                // Someone used empty constructor of our DbContext 
                optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
            }  
            base.OnConfiguring(optionsBuilder);
        }

        // Fluent API and Entities config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PlayerStatistic>( entity =>
            //{
            //    entity.HasKey(pk => new { pk.GameId, pk.PlayerId });

            //    entity.HasOne(p => p.Player)
            //          .WithMany(g => g.PlayersStatistics)
            //          .HasForeignKey(p => p.PlayerId);
            //    entity.HasOne(g => g.Game)
            //          .WithMany(p => p.PlayersStatistics)
            //          .HasForeignKey(g => g.GameId);

            //});


            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasOne(p => p.PrimaryKitColor)
                      .WithMany(t => t.PrimaryKitTeams)
                      .HasForeignKey(p => p.PrimaryKitColorId)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(s => s.SecondaryKitColor)
                      .WithMany(t => t.SecondaryKitTeams)
                      .HasForeignKey(s => s.SecondaryKitColorId)
                      .OnDelete(DeleteBehavior.NoAction);

                


            });


            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasOne(t => t.HomeTeam)
                      .WithMany(g => g.HomeGames)
                      .HasForeignKey(t => t.HomeTeamId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(t => t.AwayTeam)
                      .WithMany(g => g.AwayGames)
                      .HasForeignKey(t => t.AwayTeamId)
                      .OnDelete(DeleteBehavior.NoAction);
            });





            base.OnModelCreating(modelBuilder);
        }



    }
}