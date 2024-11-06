using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Searchers;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;

namespace BlazedWebScrapper.Data.Classes.Factories
{
    public class FactorySearcher : IFactorySearcher
    {
        public ISearcherBooks GetSearcher(string type, Query query, IBasicWebScrapperSite bws,BookServiceList bksrv)
        {
            if (type == "Czytelnik")
            {
                return new CzytelnikSearcher(query, bws,bksrv);
            }
            else if (type == "PWN")
            {
                return new PWNSearcher(query, bws,bksrv);
            }
            else if (type == "Niezwykle")
            {
                return new WydawnictwoNiezwykleSearcher(query, bws,bksrv);
            }
            else if (type == "Nasza")
            {
                return new NaszaKsiegarniaSearcher(query, bws,bksrv);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
