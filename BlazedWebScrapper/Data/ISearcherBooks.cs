namespace BlazedWebScrapper.Data
{
    public interface ISearcherBooks
    {
        void BuildFullUrlToSearch(string inputValue, string authorName, string title,string siteName);
        void SearchText(string fullUrl, IBasicWebScrapperSite webScrapperImplementation, ConstsBookScrapper consts);
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }

    }
}