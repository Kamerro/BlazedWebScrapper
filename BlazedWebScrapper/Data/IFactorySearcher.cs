namespace BlazedWebScrapper.Data
{
    public interface IFactorySearcher
    {
        public ISearcherBooks GetSearcher(string type, Query query, IBasicWebScrapperSite bws);
    }
}
