using Microsoft.EntityFrameworkCore;

namespace BlazedWebScrapper.Entities
{
	public class WebScrapperDbContext : DbContext
	{
        public DbSet<FlightModel> FlightModels { get; set; }
        public WebScrapperDbContext(DbContextOptions<WebScrapperDbContext> options) : base(options)
		{

		}
	}
}
