using BlazedWebScrapper.Data.Classes.BookHelpers;
using BlazedWebScrapper.Data.Classes.Configuration;
using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class PWNSearcher : ISearcherBooks
    {
        public PWNSearcher(Query _query, IBasicWebScrapperSite wsi, BookService bksrv)
        {
            query = _query;
            webScrapperImplementation = wsi;
            bookServiceList = bksrv;
            queryBuilder = new BookQueryHelper();
            bookDataExtraction = new BookDataExtraction("PWN",webScrapperImplementation);
        }
        HtmlDocument doc;
        HtmlWeb web;
        ConstsBookScrapper consts = new ConstsBookScrapper();
        string fullText;
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }
        public BookService bookServiceList { get; set; }
        private BookQueryHelper queryBuilder;
        BookDataExtraction bookDataExtraction;
        public void BuildFullUrlToSearch(string inputValue, string authorName, string title, string siteName)
        {
            query.ObjectOfInterest = queryBuilder.BuildObjectOfInterest(inputValue, authorName, title);
            query.UrlWithSiteName = siteName;
            webScrapperImplementation.FullUrlToReadFrom = $"{query.UrlWithSiteName}{query.ObjectOfInterest}";
        }
        public void SearchText()
        {
            SetupForSearch();
            GenerateAllImportantInfoAboutBooks();
          
        }
        private void SetupForSearch()
        {
            fullText = webScrapperImplementation.FullUrlToReadFrom;
            web = new HtmlWeb();
            doc = web.Load(fullText);
        }
        private void GenerateAllImportantInfoAboutBooks()
        {
            var paginationText = bookDataExtraction.GetPaginationPWN(doc);
            string baseURL = webScrapperImplementation.FullUrlToReadFrom;
            for (int i = 1; i <= paginationText; i++)
            {
                if (bookServiceList.FullListOfBooksPWN.Count < bookServiceList.filterSpecification.MaxResults)
                {
                    webScrapperImplementation.FullUrlToReadFrom = baseURL + $"&page={i}";
                    SetupForSearch();
                    Books = bookDataExtraction.ExtractDataPWN(doc, "", "data-productname", "div");
                    Prices = bookDataExtraction.ExtractDataPWN(doc, "", "data-productprice", "div");
                    Authors = bookDataExtraction.ExtractAuthorsPWN(doc);
                    Links = bookDataExtraction.ExtractLinksPWN(doc);
                    LinkService linkService = new LinkService(Links, consts.PWNBase);
                    linkService.GenerateLinks();
                    bookServiceList.GenerateFullListOfBooksForPWN(Books, Authors, Links, Prices);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
