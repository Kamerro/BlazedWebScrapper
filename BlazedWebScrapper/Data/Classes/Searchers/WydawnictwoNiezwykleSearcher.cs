using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;
using System.Text;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using System.Text.RegularExpressions;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class WydawnictwoNiezwykleSearcher : ISearcherBooks
    {
        public WydawnictwoNiezwykleSearcher(Query _query, IBasicWebScrapperSite wsi, BookServiceList bksrv)
        {
            query = _query;
            webScrapperImplementation = wsi;
            bookServiceList = bksrv;
        }
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }
        public BookServiceList bookServiceList { get; set; }

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
            //do constów można dodać magic stringi
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "row touchwrap", "class", "div");
            var LinkNodes = webScrapperImplementation.GetAllDescendant(BooksNodes, "div");
            LinkNodes = webScrapperImplementation.GetDescendantsWhereAttributeContains(LinkNodes, "div", "class", "product-box");
            LinkNodes = webScrapperImplementation.GetDescandant(LinkNodes, "a", 1);
            BooksNodes = webScrapperImplementation.GetAllDescendant(BooksNodes, "h6");
            Books = webScrapperImplementation.GetNamesFromNodes(BooksNodes);
            List<HtmlNode> nodePricesValues = webScrapperImplementation.AllNodes(doc, "cena-box", "class", "div");
            nodePricesValues = webScrapperImplementation.GetFirstDescendant(nodePricesValues, "p", "span", "strike");
            Prices = webScrapperImplementation.GetNamesFromNodes(nodePricesValues);
            var AuthorsNodes = webScrapperImplementation.AllNodes(doc, "desc", "class", "div");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "p");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "a");
            Authors = webScrapperImplementation.GetNamesFromNodes(AuthorsNodes);
            Links = webScrapperImplementation.GetStringFromAttribute(LinkNodes, "href");

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
