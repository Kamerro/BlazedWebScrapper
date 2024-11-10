using BlazedWebScrapper.Data.Classes.BookHelpers;
using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Extension_methods;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class CzytelnikSearcher : ISearcherBooks
    {
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
        public CzytelnikSearcher(Query _query, IBasicWebScrapperSite wsi, BookService bksrv)
        {
            query = _query;
            webScrapperImplementation = wsi;
            bookServiceList = bksrv;
            queryBuilder = new BookQueryHelper();
        }
        public void BuildFullUrlToSearch(string inputValue, string authorName, string title, string siteName)
        {
            query.ObjectOfInterest = queryBuilder.BuildObjectOfInterest(inputValue, authorName, title);
            query.UrlWithSiteName = siteName;
            webScrapperImplementation.FullUrlToReadFrom = $"{query.UrlWithSiteName}{query.ObjectOfInterest}/1/phot/5?url={query.ObjectOfInterest}";
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
            LinkService linkService = new LinkService(Links, consts.CzytelnikBase);
            linkService.GenerateLinks();
            bookServiceList.GenerateFullListOfBooks(Books, Authors, Links, Prices);
        }

        private void GenerateAllImportantInfoAboutBooks()
        {
            Books = ExtractBooks();
            Prices = ExtractPrices();
            Authors = ExtractAuthors();
            Links = ExtractLinks();
        }
        private List<string> ExtractLinks()
        {
            var linkNode = webScrapperImplementation.AllNodes(doc, "prodname f-row", "class", "a");
            return webScrapperImplementation.GetStringFromAttribute(linkNode, "href");
        }
        private List<string> ExtractAuthors()
        {
            var AuthorNameNodes = webScrapperImplementation.AllNodes(doc, "brand", "class", "a");
            Authors = webScrapperImplementation.GetNamesFromNodes(AuthorNameNodes);
            return Authors.LeaveOnlyAuthorName();
        }
        private List<string> ExtractBooks()
        {
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "productname", "class", "span");
            return webScrapperImplementation.GetNamesFromNodes(BooksNodes);
        }
        private List<string> ExtractPrices()
        {
            List<HtmlNode> nodePricesDiv = webScrapperImplementation.AllNodes(doc, "price f-row", "class", "div");
            List<HtmlNode> nodePricesValue = webScrapperImplementation.GetFirstDescendant(nodePricesDiv, "em");
            return webScrapperImplementation.GetNamesFromNodes(nodePricesValue);
        }
    }
}
