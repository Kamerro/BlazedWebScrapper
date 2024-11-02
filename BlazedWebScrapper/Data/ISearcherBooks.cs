namespace BlazedWebScrapper.Data
{
    public interface ISearcherBooks
    {
        string BuildFullUrlToSearch(Query query, string inputValue, string authorName, string title);
        void SearchText(string fullUrl, IBasicWebScrapperSite webScrapperImplementation, ConstsBookScrapper consts);
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }

    }
}