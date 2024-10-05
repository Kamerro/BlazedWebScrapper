using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazedWebScrapper.Entities
{
    public class FlightModel
    {
        public int Id { get; set; }
        public string StartDestination { get; set; }
        public string EndDestination { get; set; }
        public string StartTripDayOfWeek { get; set; }
        public string EndTripDayOfWeek { get; set; }
        public DateTime StartTripDeparture { get; set; }
        public DateTime StartTripArrival { get; set; }
        public DateTime EndTripDeparture { get; set; }
        public DateTime EndTripArrival { get; set; }
        public TimeSpan TimeOfStartTrip { get; set; }
        public TimeSpan TimeOfEndTrip { get; set; }
        public float StartTripPrice { get; set; }
        public float EndTripPrice { get; set; }
        public float Price { get; set; }
    }
}
