using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Searchers;
using BlazedWebScrapper.Data.Interfaces;

namespace BlazedWebScrapper.Data.Classes.Factories
{
    public class FactorySearcher : IFactorySearcher
    {
        public ISearcherBooks GetSearcher(string type, Query query, IBasicWebScrapperSite bws)
        {
            if (type == "Czytelnik")
            {
                return new CzytelnikSearcher(query, bws);
            }
            else if (type == "PWN")
            {
                return new PWNSearcher(query, bws);
            }
            else if (type == "Niezwykle")
            {
                return new WydawnictwoNiezwykleSearcher(query, bws);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
