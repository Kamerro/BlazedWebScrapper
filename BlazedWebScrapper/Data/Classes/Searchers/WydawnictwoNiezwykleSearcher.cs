using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using System.Text.RegularExpressions;
using BlazedWebScrapper.Data.Classes.BookHelpers;
using Microsoft.AspNetCore.Http.Extensions;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class WydawnictwoNiezwykleSearcher : ISearcherBooks
    {
        public WydawnictwoNiezwykleSearcher(Query _query, IBasicWebScrapperSite wsi, BookService bksrv)
        {
            query = _query;
            webScrapperImplementation = wsi;
            bookServiceList = bksrv;
            queryBuilder = new BookQueryHelper();
            bookDataExtraction = new BookDataExtraction("WN", webScrapperImplementation);

        }
        BookDataExtraction bookDataExtraction;

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

        public void BuildFullUrlToSearch(string inputValue, string authorName, string title, string siteName)
        {
            query.ObjectOfInterest = queryBuilder.BuildObjectOfInterest(inputValue, authorName, title);
            query.UrlWithSiteName = siteName;
            webScrapperImplementation.FullUrlToReadFrom = $"{query.UrlWithSiteName}{query.ObjectOfInterest}";
        }
        private void SetupForSearch()
        {
            fullText = webScrapperImplementation.FullUrlToReadFrom;
            web = new HtmlWeb();
            doc = web.Load(fullText);
        }
        public void SearchText()
        {
            SetupForSearch();
            GenerateAllImportantInfoAboutBooks();
        }
        private void GenerateAllImportantInfoAboutBooks()
        {
            var paginationText = bookDataExtraction.GetPaginationWN(doc);
            string baseURL = webScrapperImplementation.FullUrlToReadFrom;
            for (int i = 1; i <= paginationText; i++)
            {
                if ((bookServiceList.FullListOfBooksWN.Count < bookServiceList.filterSpecification.MaxResults) || bookServiceList.filterSpecification.MaxResults == 0)
                {
                    webScrapperImplementation.FullUrlToReadFrom = baseURL + $"/{i}";
                    Books = bookDataExtraction.ExtractBooksWN(doc);
                    Prices = bookDataExtraction.ExtractPricesWN(doc);
                    Authors = bookDataExtraction.ExtractAuthorsWN(doc);
                    Links = bookDataExtraction.ExtractLinksWN(doc);
                    LinkService linkService = new LinkService(Links, consts.NiezwykleBase);
                    linkService.GenerateLinks();
                    bookServiceList.GenerateFullListOfBooksForWN(Books, Authors, Links, Prices);
                }
            }
        }
    }
}
