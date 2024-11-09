using BlazedWebScrapper.Data.Classes.BookHelpers;
using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class PWNSearcher : ISearcherBooks
    {
        public PWNSearcher(Query _query, IBasicWebScrapperSite wsi, BookServiceList bksrv)
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
        public BookServiceList bookServiceList { get; set; }
        private BookQueryHelper queryBuilder;
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
            var pagination = webScrapperImplementation.AllNodes(doc, "pagination", "class", "div");
            var paginationNode = webScrapperImplementation.GetNearEndDescandant(pagination,"li");
            pagination = webScrapperImplementation.GetFirstDescendant( new List<HtmlNode>() { paginationNode }, "a");
            var paginationText = webScrapperImplementation.GetNamesFromNodes(pagination)[0];
            for (int i = 1; i <= int.Parse(paginationText); i++)
            {
                fullText = fullText + ($"&page={paginationText}&sortBy=score");
                Books = ExtractData("", "data-productname", "div");
                Prices = ExtractData("", "data-productprice", "div");
                Authors = ExtractAuthors();
                Links = ExtractLinks();
                LinkService linkService = new LinkService(Links, consts.PWNBase);
                linkService.GenerateLinks();
                bookServiceList.GenerateFullListOfBooks(Books, Authors, Links, Prices);
            }
        }
        private List<string> ExtractLinks()
        {
            var linkNode = webScrapperImplementation.AllNodes(doc, "item-action-url", "class", "a");
            return webScrapperImplementation.GetStringFromAttribute(linkNode, "href").Where((element, index) => index % 4 == 0).ToList();
        }

        private List<string> ExtractData(string name,string attribute,string htmlTag)
        {
            var Nodes = webScrapperImplementation.AllNodes(doc, name, attribute, htmlTag);
            return webScrapperImplementation.GetStringFromAttribute(Nodes, attribute);
        }

        private List<string> ExtractAuthors()
        {
            var AuthorNodes = webScrapperImplementation.AllNodes(doc, "emp-info-authors", "class", "div");
            var nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(AuthorNodes, "span");
            nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(AuthorNodes, "a");
            return webScrapperImplementation.GetNamesFromNodes(nodeAuthorValue).Where((element, index) => index % 2 == 0).ToList();
        }
    }
}
