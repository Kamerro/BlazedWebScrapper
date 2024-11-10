using BlazedWebScrapper.Entities;
using System.Text;

namespace BlazedWebScrapper.Data.Flight
{
    public static class MailTextFormatter
    {
        public static string GetText(List<FlightModel> flights)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Propozycje tanich lotów na dziś<br><br>");

            foreach (var flight in flights)
            {
                sb.Append(GetTextForItem(flight));
            }
            return sb.ToString();
        }

        private static string GetTextForItem(FlightModel flight)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format("Wylot: {0} ==> {1} <br>[{7}] {2} ==> {3} <br>Powrót: {1} ==> {0} <br>[{8}] {4} ==> {5} <br>Cena: {6}zł<br><br>",
                    flight.StartDestination,
                    flight.EndDestination,
                    flight.StartTripDeparture,
                    flight.StartTripArrival,
                    flight.EndTripDeparture,
                    flight.EndTripDeparture,
                    flight.Price,
                    flight.StartTripDayOfWeek,
                    flight.EndTripDayOfWeek));

            return sb.ToString();
        }
    }
}