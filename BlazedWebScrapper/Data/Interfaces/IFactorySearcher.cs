using BlazedWebScrapper.Data.Classes.Queries;
namespace BlazedWebScrapper.Data.Interfaces
{
    public interface IFactorySearcher
    {
        public ISearcherBooks GetSearcher(string type, Query query, IBasicWebScrapperSite bws);
    }
}
