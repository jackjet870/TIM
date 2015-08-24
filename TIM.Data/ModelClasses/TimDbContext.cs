namespace TIM.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TimDbContext : DbContext
    {
        public TimDbContext()
            : base("name=TimDbContext")
        {
        }

        public virtual DbSet<Athlete> Athlete { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.Sport)
                .IsUnicode(false);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.AthleteId)
                .HasPrecision(28, 0);

            modelBuilder.Entity<Athlete>()
                .Property(e => e.TeamId)
                .HasPrecision(28, 0);

            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.Sport)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventId)
                .HasPrecision(28, 0);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Athlete)
                .WithMany(e => e.Event)
                .Map(m => m.ToTable("AthletesEvents").MapLeftKey("EventId").MapRightKey("AthleteId"));

            modelBuilder.Entity<Team>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Sport)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.TeamId)
                .HasPrecision(28, 0);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Event)
                .WithMany(e => e.Team)
                .Map(m => m.ToTable("TeamsEvents").MapLeftKey("TeamId").MapRightKey("EventId"));

            modelBuilder.Entity<User>()
                .Property(e => e.User_ID)
                .HasPrecision(28, 0);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Athlete)
                .WithMany(e => e.User)
                .Map(m => m.ToTable("UsersFavAthletes").MapLeftKey("UserId").MapRightKey("AthleteId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.Event)
                .WithMany(e => e.User)
                .Map(m => m.ToTable("UsersFavEvents").MapLeftKey("UserId").MapRightKey("EventId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.Team)
                .WithMany(e => e.User)
                .Map(m => m.ToTable("UsersFavTeams").MapLeftKey("UserId").MapRightKey("TeamId"));
        }
    }
}
