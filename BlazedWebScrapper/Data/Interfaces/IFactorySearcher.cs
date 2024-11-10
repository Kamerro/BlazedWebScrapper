using BlazedWebScrapper.Data.Classes.Configuration;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;

namespace BlazedWebScrapper.Data.Interfaces
{
    public interface IFactorySearcher
    {
        public ISearcherBooks GetSearcher(string type, Query query, IBasicWebScrapperSite bws,BookService bksrv);
    }
}
