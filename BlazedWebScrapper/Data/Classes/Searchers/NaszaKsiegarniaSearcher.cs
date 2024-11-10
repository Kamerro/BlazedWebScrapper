﻿using BlazedWebScrapper.Data.Classes.BookHelpers;
using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Extension_methods;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class NaszaKsiegarniaSearcher : ISearcherBooks
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

        public NaszaKsiegarniaSearcher(Query _query, IBasicWebScrapperSite wsi, BookService bksrv)
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
            LinkService linkService = new LinkService(Links, consts.NaszaKsiegarniaBase);
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
            var LinkNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            LinkNodes = webScrapperImplementation.GetFirstDescendant(LinkNodes, "h2");
            LinkNodes = webScrapperImplementation.GetFirstDescendant(LinkNodes, "a");
            return webScrapperImplementation.GetStringFromAttribute(LinkNodes, "href");

        }
        private List<string> ExtractAuthors()
        {
            var AuthorNameNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "h4");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "span");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "a");
            Authors = webScrapperImplementation.GetNamesFromNodes(AuthorNameNodes);
            return Authors.LeaveOnlyAuthorName();
        }
        private List<string> ExtractBooks()
        {
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            BooksNodes = webScrapperImplementation.GetFirstDescendant(BooksNodes, "h2");
            BooksNodes = webScrapperImplementation.GetFirstDescendant(BooksNodes, "a");
            return webScrapperImplementation.GetNamesFromNodes(BooksNodes);
        }
        private List<string> ExtractPrices()
        {
            List<HtmlNode> nodePricesDiv = webScrapperImplementation.AllNodes(doc, "price", "class", "section");
            List<HtmlNode> nodePricesValue = webScrapperImplementation.GetDescandant(nodePricesDiv, "span", 1);
            if (nodePricesValue.Count == 0)
            {
                nodePricesValue = webScrapperImplementation.GetDescandant(nodePricesDiv, "span", 0);
            }
            return webScrapperImplementation.GetNamesFromNodes(nodePricesValue);
        }
    }
}
