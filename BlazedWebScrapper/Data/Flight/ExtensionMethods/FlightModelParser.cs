using BlazedWebScrapper.Entities;
using Microsoft.IdentityModel.Tokens;
using WebScrapper;

namespace BlazedWebScrapper.Data.Flight.ExtensionMethods
{
    public static class FlightModelParser
    {
        public static FlightModel Parse(this ThereBackFlight thereBackFlight)
        {
            string[] thereParts = GetThereElementsToWords(thereBackFlight);
            string[] backParts = GetBackElementsToWords(thereBackFlight);

            List<int> thereTimesIndex = GetIndexOfThereTimes(thereParts);
            List<int> backTimesIndex = GetIndexOfBackTimes(backParts);

            FlightModel thereFlightModel = CreateTherePartOfModel(thereParts, thereTimesIndex);
            FlightModel backFlightModel = CreateBackPartOfModel(backParts, backTimesIndex);

            FlightModel flightModel = ConcatModels(thereFlightModel, backFlightModel);

            return flightModel;
        }

        private static string[] GetThereElementsToWords(ThereBackFlight thereBackFlight)
        {
            string[] thereParts = thereBackFlight.There.Split(' ');
            return thereParts;
        }

        private static string[] GetBackElementsToWords(ThereBackFlight thereBackFlight)
        {
            string[] backParts = thereBackFlight.Back.Split(' ');
            return backParts;
        }

        private static List<int> GetIndexOfThereTimes(string[] thereParts)
        {
            List<int> thereTimesIndex = new List<int>();
            // wyszukanie ":" czasów There
            for (int i = 0; i < thereParts.Length; i++)
            {
                if (thereParts[i].Contains(":")) thereTimesIndex.Add(i);
            }

            return thereTimesIndex;
        }

        private static List<int> GetIndexOfBackTimes(string[] backParts)
        {
            List<int> backTimesIndex = new List<int>();
            // wyszukanie ":" czasów Back
            for (int i = 0; i < backParts.Length; i++)
            {
                if (backParts[i].Contains(":")) backTimesIndex.Add(i);
            }

            return backTimesIndex;
        }

        private static FlightModel CreateTherePartOfModel(string[] thereParts, List<int> thereTimesIndex)
        {
            FlightModel model = new FlightModel();

            // tworzenie modelu There
            model.StartTripDayOfWeek = thereParts[1];
            model.TimeOfStartTrip = TimeOnly.ParseExact(thereParts[thereTimesIndex[2]], "H:mm");

            DateOnly startDate = DateOnly.ParseExact(thereParts[2], "dd/MM/yy");
            TimeOnly startDepartureTime = TimeOnly.ParseExact(thereParts[thereTimesIndex[0]], "HH:mm");
            TimeOnly startArrivalTime = TimeOnly.ParseExact(thereParts[thereTimesIndex[1]], "HH:mm");

            model.StartTripDeparture = startDate.ToDateTime(startDepartureTime);
            model.StartTripArrival = startDate.ToDateTime(startArrivalTime);

            string startDestination = null;
            string endDestination = null;
            for (int i = 0; i < thereParts.Length; i++)
            {
                if (i > thereTimesIndex[0] && i < thereTimesIndex[1])
                    model.StartDestination += thereParts[i] + " ";

                if (i > thereTimesIndex[1] && i < thereTimesIndex[2])
                    model.EndDestination += thereParts[i] + " ";
            }

            model.StartTripPrice = float.Parse(thereParts[thereParts.Length - 3]);

            return model;
        }

        private static FlightModel CreateBackPartOfModel(string[] backParts, List<int> backTimesIndex)
        {
            FlightModel model = new FlightModel();

            model.EndTripDayOfWeek = backParts[1];
            model.TimeOfEndTrip = TimeOnly.ParseExact(backParts[backTimesIndex[2]], "H:mm");

            DateOnly endDate = DateOnly.ParseExact(backParts[2], "dd/MM/yy");
            TimeOnly endDepartureTime = TimeOnly.ParseExact(backParts[backTimesIndex[0]], "HH:mm");
            TimeOnly endArrivalTime = TimeOnly.ParseExact(backParts[backTimesIndex[1]], "HH:mm");

            model.EndTripDeparture = endDate.ToDateTime(endDepartureTime);
            model.EndTripArrival = endDate.ToDateTime(endArrivalTime);

            model.EndTripPrice = float.Parse(backParts[backParts.Length - 3]);

            return model;
        }

        private static FlightModel ConcatModels(FlightModel thereFlightModel, FlightModel backFlightmodel) 
        {
            FlightModel flightModel = new FlightModel()
            {
                StartTripDayOfWeek = thereFlightModel.StartTripDayOfWeek,
                StartTripDeparture = thereFlightModel.StartTripDeparture,
                StartTripArrival = thereFlightModel.StartTripArrival,
                StartDestination = thereFlightModel.StartDestination,
                EndDestination = thereFlightModel.EndDestination,
                TimeOfStartTrip = thereFlightModel.TimeOfStartTrip,
                TimeOfEndTrip = backFlightmodel.TimeOfEndTrip,
                EndTripDayOfWeek = backFlightmodel.EndTripDayOfWeek,
                EndTripDeparture = backFlightmodel.EndTripDeparture,
                EndTripArrival = backFlightmodel.EndTripArrival,
                StartTripPrice = thereFlightModel.StartTripPrice,
                EndTripPrice = backFlightmodel.EndTripPrice,
                Price = thereFlightModel.StartTripPrice + backFlightmodel.EndTripPrice  // Sum of both legs
            };

            return flightModel;
        }
    }
}



