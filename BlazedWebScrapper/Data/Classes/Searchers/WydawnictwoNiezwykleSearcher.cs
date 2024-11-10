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
            LinkService linkService = new LinkService(Links, consts.NiezwykleBase);
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
            var LinkNodes = webScrapperImplementation.AllNodes(doc, "row touchwrap", "class", "div");
            LinkNodes = webScrapperImplementation.GetDescendantsWhereAttributeContains(LinkNodes, "div", "class", "product-box");
            LinkNodes = webScrapperImplementation.GetDescandant(LinkNodes, "a", 1);
            return webScrapperImplementation.GetStringFromAttribute(LinkNodes, "href");
        }
        private List<string> ExtractAuthors()
        {
            var AuthorsNodes = webScrapperImplementation.AllNodes(doc, "desc", "class", "div");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "p");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "a");
            return webScrapperImplementation.GetNamesFromNodes(AuthorsNodes);
        }
        private List<string> ExtractBooks()
        {
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "row touchwrap", "class", "div");
            BooksNodes = webScrapperImplementation.GetAllDescendant(BooksNodes, "h6");
            return webScrapperImplementation.GetNamesFromNodes(BooksNodes);
        }
        private List<string> ExtractPrices()
        {
            List<HtmlNode> nodePricesValues = webScrapperImplementation.AllNodes(doc, "cena-box", "class", "div");
            nodePricesValues = webScrapperImplementation.GetFirstDescendant(nodePricesValues, "p", "span", "strike");
            return webScrapperImplementation.GetNamesFromNodes(nodePricesValues);
        }
    }
}
