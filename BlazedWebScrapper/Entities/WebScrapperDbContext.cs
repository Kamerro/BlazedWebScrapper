using Microsoft.EntityFrameworkCore;

namespace BlazedWebScrapper.Entities
{
	public class WebScrapperDbContext : DbContext
	{
        public DbSet<FlightModel> FlightModels { get; set; }
        public WebScrapperDbContext(DbContextOptions<WebScrapperDbContext> options) : base(options)
		{

		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapowanie TimeOnly jako TimeSpan
            modelBuilder.Entity<FlightModel>()
                .Property(f => f.TimeOfStartTrip)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v)
                );

            modelBuilder.Entity<FlightModel>()
                .Property(f => f.TimeOfEndTrip)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v)
                );
        }
    }
}
