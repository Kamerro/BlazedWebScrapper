namespace BlazedWebScrapper.Data
{
    public interface ISearcherBooks
    {
        string BuildFullUrlToSearch(Query query, string inputValue, string authorName, string title);
        void SearchText(string fullUrl, IBasicWebScrapperSite webScrapperImplementation, ConstsBookScrapper consts);
        public List<string> BooksNames { get; set; }
        public List<string> PricePerBook { get; set; }
        public List<string> AuthorName { get; set; }
    }
}