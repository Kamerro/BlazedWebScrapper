using BlazedWebScrapper.Entities;
using WebScrapper;

namespace BlazedWebScrapper.Data.Flight
{
    public class ParseToFlightModel
    {
        private ThereBackFlight _thereBackFlight;

        private List<int> thereTimesIndex;
        private List<int> backTimesIndex;
        private string[] thereParts;
        private string[] backParts;

        public ParseToFlightModel(ThereBackFlight thereBackFlight)
        {
            _thereBackFlight = thereBackFlight;
        }

        static public FlightModel Parse(ThereBackFlight thereBackFlight)
        {
            return null;
        }

        private string[] GetThereElementsToWords(ThereBackFlight thereBackFlight)
        {
            string[] thereParts = thereBackFlight.There.Split(' ');
            return thereParts;
        }

        private string[] GetBackElementsToWords(ThereBackFlight thereBackFlight)
        {
            string[] backParts = thereBackFlight.Back.Split(' ');
            return backParts;
        }
    }
}
