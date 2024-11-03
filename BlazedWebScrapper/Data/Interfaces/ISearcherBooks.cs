using BlazedWebScrapper.Data.Classes.Queries;

namespace BlazedWebScrapper.Data.Interfaces
{
    public interface ISearcherBooks
    {
        void BuildFullUrlToSearch(string inputValue, string authorName, string title, string siteName);
        void SearchText();
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }

    }
}