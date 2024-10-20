using System.Text;
using WebScrapper;

namespace BlazedWebScrapper.Data.Flight
{
    public static class ThereBackFlightParser
    {
        static bool thereFlag;
        static bool backFlag;
        static StringBuilder there = new StringBuilder();
        static StringBuilder back = new StringBuilder();

        public static ThereBackFlight GetFlightInfo(string scrappedText)
        {
            
            var words = scrappedText.Split(" ");

            foreach (var word in words)
            {
                if (word == "There") thereFlag = true;
                if (word == "Back") backFlag = true;

                if (thereFlag) there.Append(word + " ");
                if (backFlag) back.Append(word + " ");

                if (word == "zł\n\n")
                {
                    thereFlag = false; backFlag = false;
                    if (there != null) there = there.Replace("\n", "");
                    if (back != null) back = back.Replace("\n", "");
                }
            }

            return new ThereBackFlight(there.ToString(), back.ToString());
        }
    }
}
