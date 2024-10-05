using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazedWebScrapper.Migrations
{
    /// <inheritdoc />
    public partial class addedFlightModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDestination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDestination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTripDayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTripDayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTripDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTripArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTripDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTripArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOfStartTrip = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeOfEndTrip = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartTripPrice = table.Column<float>(type: "real", nullable: false),
                    EndTripPrice = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightModels");
        }
    }
}
