using Microsoft.EntityFrameworkCore;

namespace _253503_Ankuda.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            _options = options;

            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamEntity>()
                        .HasMany(p => p.Competitors)
                        .WithOne(t => t.Team)
                        .HasForeignKey(t => t.TeamId);
            modelBuilder.Entity<CompetitorEntity>()
                .HasOne(p => p.Team);

        }

    }
}