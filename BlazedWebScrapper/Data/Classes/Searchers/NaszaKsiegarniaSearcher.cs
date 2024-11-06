using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Data;
using BlazedWebScrapper.Data.Classes.Extension_methods;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class NaszaKsiegarniaSearcher : ISearcherBooks
    {
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }
        public BookServiceList bookServiceList { get; set; }

        public NaszaKsiegarniaSearcher(Query _query, IBasicWebScrapperSite wsi, BookServiceList bksrv)
        {
            query = _query;
            webScrapperImplementation = wsi;
            bookServiceList = bksrv;
        }
        public void BuildFullUrlToSearch(string inputValue, string authorName, string title, string siteName)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(inputValue))
            {
                sb.Append(authorName);
                sb.Append(" ");
                sb.Append(title);
            }
            else
            {
                sb.Append(inputValue);
            }
            query.ObjectOfInterest = sb.ToString();
            query.UrlWithSiteName = siteName;
            webScrapperImplementation.FullUrlToReadFrom = $"{query.UrlWithSiteName}{query.ObjectOfInterest}";
        }
        public void SearchText()
        {
            ConstsBookScrapper consts = new ConstsBookScrapper();
            string fullText = webScrapperImplementation.FullUrlToReadFrom;
            var web = new HtmlWeb();
            var doc = web.Load(fullText);
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            BooksNodes = webScrapperImplementation.GetFirstDescendant(BooksNodes, "h2");
            BooksNodes = webScrapperImplementation.GetFirstDescendant(BooksNodes, "a");
            Links = webScrapperImplementation.GetStringFromAttribute(BooksNodes, "href");
            Books = webScrapperImplementation.GetNamesFromNodes(BooksNodes);
            List<HtmlNode> nodePricesDiv = webScrapperImplementation.AllNodes(doc, "price", "class", "section");
            List<HtmlNode> nodePricesValue = webScrapperImplementation.GetDescandant(nodePricesDiv, "span", 1);
            if (nodePricesValue.Count == 0)
            {
                nodePricesValue = webScrapperImplementation.GetDescandant(nodePricesDiv, "span", 0);
            }
            Prices = webScrapperImplementation.GetNamesFromNodes(nodePricesValue);
            var AuthorNameNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "h4");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "span");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "a");
            Authors = webScrapperImplementation.GetNamesFromNodes(AuthorNameNodes);
            Authors = Authors.LeaveOnlyAuthorName();
            for (int i = 0; i < Links.Count; i++)
            {
                Links[i] = consts.NaszaKsiegarniaBase + Links[i];
            }

            for (int i = 0; i < Books.Count; i++)
            {
                var match = Regex.Match(Prices[i], @"\d+([.,]\d{1,2})?");
                if (match.Success)
                {
                    string filteredValue = match.Value.Replace(".", ",");

                    if (decimal.TryParse(filteredValue, out decimal price))
                    {
                        bookServiceList.FullListOfBooks.Add(new($"{this.Authors[i]}-{Books[i]}", price, Links[i]));
                    }
                }

            }

        }
    }
}
