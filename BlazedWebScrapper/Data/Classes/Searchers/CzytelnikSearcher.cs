using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Extension_methods;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class CzytelnikSearcher : ISearcherBooks
    {
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }

        public BookServiceList bookServiceList { get; set; }


        public CzytelnikSearcher(Query _query, IBasicWebScrapperSite wsi, BookServiceList bksrv)
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
            webScrapperImplementation.FullUrlToReadFrom = $"{query.UrlWithSiteName}{query.ObjectOfInterest}/1/phot/5?url={query.ObjectOfInterest}";
        }
        public void SearchText()
        {
            ConstsBookScrapper consts = new ConstsBookScrapper();
            string fullText = webScrapperImplementation.FullUrlToReadFrom;
            var web = new HtmlWeb();
            var doc = web.Load(fullText);
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "productname", "class", "span");
            Books = webScrapperImplementation.GetNamesFromNodes(BooksNodes);
            List<HtmlNode> nodePricesDiv = webScrapperImplementation.AllNodes(doc, "price f-row", "class", "div");
            List<HtmlNode> nodePricesValue = webScrapperImplementation.GetFirstDescendant(nodePricesDiv, "em");
            Prices = webScrapperImplementation.GetNamesFromNodes(nodePricesValue);
            var AuthorNameNodes = webScrapperImplementation.AllNodes(doc, "brand", "class", "a");
            Authors = webScrapperImplementation.GetNamesFromNodes(AuthorNameNodes);
            Authors = Authors.LeaveOnlyAuthorName();
            var linkNode = webScrapperImplementation.AllNodes(doc, "prodname f-row", "class", "a");
            Links = webScrapperImplementation.GetStringFromAttribute(linkNode, "href");
            for (int i = 0; i < Links.Count; i++)
            {
                Links[i] = consts.CzytelnikBase + Links[i];
            }

            for(int i = 0; i < Books.Count && i < Authors.Count && i < Links.Count; i++)
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
