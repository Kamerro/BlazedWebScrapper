using BlazedWebScrapper.Entities;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebScrapper;

namespace BlazedWebScrapper.Data
{
    public class FlightService
	{
		private WebScrapperDbContext _dbContext;
		private EmailSender _email;
		string url = "https://www.azair.eu/azfin.php?searchtype=flexi&tp=0&isOneway=return&srcAirport=Poland+%5BSZY%5D+%28%2BKRK%2CKTW%29&srcap10=KRK&srcap11=KTW&srcFreeAirport=&srcTypedText=po&srcFreeTypedText=&srcMC=PL&dstAirport=Anywhere+%5BXXX%5D&anywhere=true&dstap0=LIN&dstap1=BGY&dstFreeAirport=&dstTypedText=xxx&dstFreeTypedText=&dstMC=&depmonth=202410&depdate=2024-10-03&aid=0&arrmonth=202412&arrdate=2024-12-31&minDaysStay=5&maxDaysStay=8&dep0=true&dep1=true&dep2=true&dep3=true&dep4=true&dep5=true&dep6=true&arr0=true&arr1=true&arr2=true&arr3=true&arr4=true&arr5=true&arr6=true&samedep=true&samearr=true&minHourStay=0%3A45&maxHourStay=23%3A20&minHourOutbound=0%3A00&maxHourOutbound=24%3A00&minHourInbound=0%3A00&maxHourInbound=24%3A00&autoprice=true&adults=1&children=0&infants=0&maxChng=1&currency=PLN&lang=en&indexSubmit=Search";

        public FlightService(WebScrapperDbContext dbContext, EmailSender email)
        {
            _dbContext = dbContext;
			_email = email;
        }

        public List<FlightModel> GetFlights()
        {
			var web = new HtmlWeb();
			var document = web.Load(url);

			var tableRows = document.QuerySelectorAll("#reslist .result");

			List<FlightModel> flightModels = new List<FlightModel>();
			foreach (var row in tableRows)
			{
				var divText = row.InnerText;

				var flightInfo = GetThereBackFlight(divText);
				flightModels.Add(ParseFlightDetails(flightInfo));
            }

            // Usuwanie i Dodanie lotów do bazy danych
            var allFlights = _dbContext.FlightModels.ToList();
            _dbContext.FlightModels.RemoveRange(allFlights);
            _dbContext.FlightModels.AddRange(flightModels);
            _dbContext.SaveChanges();

            // Tworzenie maila
            List<FlightModel> cheepestFlight = null;

            // LINQ
            cheepestFlight = _dbContext
            .FlightModels
            .OrderBy(f => f.Price)
            .Take(5)
            .ToList();

			var emailReceiver = "zbigniew.sr@interia.pl";

            var email = new EmailSender(new EmailParams
            {
                HostSmtp = "poczta.interia.pl",
                Port = 587,
                EnableSsl = true,
                SenderName = "Webscrapper Info",
                SenderEmail = "webscrapper.mail@interia.pl",
                SenderEmailPassword = "WEBscrapper123!"
            });

            var mailText = MailTextFormatter(cheepestFlight);
			
            email.Send(
                "Tanie loty",
                mailText,
                emailReceiver);

            return _dbContext.FlightModels.ToList(); // użycie asynchronicznej metody
        }


        //public async Task<string> ProcessInput(string inputText)
        //{
            
        //}

        public string MailTextFormatter(List<FlightModel> flights)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Propozycje tanich lotów na dziś<br><br>");

			foreach (var flight in flights)
			{
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
			}
			return sb.ToString();
		}

		public FlightModel ParseFlightDetails(FlightInfo flightDetails)
		{
			string thereDetails = flightDetails.There;
			string backDetails = flightDetails.Back;

			List<int> thereTimesIndex = new List<int>();
			List<int> backTimesIndex = new List<int>();

			// Parse "There" details
			string[] thereParts = thereDetails.Split(' ');

			// wyszukanie ":" czasów There
			for (int i = 0; i < thereParts.Length; i++)
			{
				if (thereParts[i].Contains(":")) thereTimesIndex.Add(i);
			}

			// tworzenie modelu There
			string startDayOfWeek = thereParts[1];
			DateOnly startDate = DateOnly.ParseExact(thereParts[2], "dd/MM/yy");
			TimeOnly startDepartureTime = TimeOnly.ParseExact(thereParts[thereTimesIndex[0]], "HH:mm");
			TimeOnly startArrivalTime = TimeOnly.ParseExact(thereParts[thereTimesIndex[1]], "HH:mm");
            TimeOnly timeOfStartTrip = TimeOnly.ParseExact(thereParts[thereTimesIndex[2]], "H:mm");

			string startDestination = null;
			string endDestination = null;
			for (int i = 0; i < thereParts.Length; i++)
			{
				if (i > thereTimesIndex[0] && i < thereTimesIndex[1])
					startDestination += thereParts[i] + " ";

				if (i > thereTimesIndex[1] && i < thereTimesIndex[2])
					endDestination += thereParts[i] + " ";
			}

			float startTripPrice = float.Parse(thereParts[thereParts.Length - 3]);

			// Parse "Back" details
			string[] backParts = backDetails.Split(' ');

			// wyszukanie ":" czasów Back
			for (int i = 0; i < backParts.Length; i++)
			{
				if (backParts[i].Contains(":")) backTimesIndex.Add(i);
			}

			string endDayOfWeek = backParts[1];
			DateOnly endDate = DateOnly.ParseExact(backParts[2], "dd/MM/yy");
			TimeOnly endDepartureTime = TimeOnly.ParseExact(backParts[backTimesIndex[0]], "HH:mm");
			TimeOnly endArrivalTime = TimeOnly.ParseExact(backParts[backTimesIndex[1]], "HH:mm");
            TimeOnly timeOfEndTrip = TimeOnly.ParseExact(backParts[backTimesIndex[2]], "H:mm");

			float endTripPrice = float.Parse(backParts[backParts.Length - 3]);

			FlightModel flightModel = new FlightModel()
			{
				StartTripDayOfWeek = startDayOfWeek,
				StartTripDeparture = startDate.ToDateTime(startDepartureTime),
				StartTripArrival = startDate.ToDateTime(startArrivalTime),
				StartDestination = startDestination,
				EndDestination = endDestination,
				TimeOfStartTrip = timeOfStartTrip,
				TimeOfEndTrip = timeOfEndTrip,
				EndTripDayOfWeek = endDayOfWeek,
				EndTripDeparture = endDate.ToDateTime(endDepartureTime),
				EndTripArrival = endDate.ToDateTime(endArrivalTime),
				StartTripPrice = startTripPrice,
				EndTripPrice = endTripPrice,
				Price = startTripPrice + endTripPrice  // Sum of both legs
			};

			return flightModel;
		}

		public FlightInfo GetThereBackFlight(string divText)
		{
			bool thereFlag = false;
			bool backFlag = false;
			string there = null;
			string back = null;
			var words = divText.Split(" ");

			foreach (var word in words)
			{
				if (word == "There") thereFlag = true;
				if (word == "Back") backFlag = true;

				if (thereFlag) there += word + " ";
				if (backFlag) back += word + " ";

				if (word == "zł\n\n")
				{
					thereFlag = false; backFlag = false;
					if (there != null) there = there.Replace("\n", "");
					if (back != null) back = back.Replace("\n", "");
				}
			}

			return new FlightInfo(there, back);
		}
	}
}
