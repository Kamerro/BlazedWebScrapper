using HtmlAgilityPack;
using System.Text;

namespace BlazedWebScrapper.Data
{
    public class WydawnictwoNiezwykleSearcher : ISearcherBooks
    {
        public WydawnictwoNiezwykleSearcher(Query _query, IBasicWebScrapperSite wsi)
        {
            query = _query;
            webScrapperImplementation = wsi;
        }
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }
        public void BuildFullUrlToSearch(string inputValue, string authorName, string title, string siteName)
        {
            StringBuilder sb = new StringBuilder();
            if (String.IsNullOrEmpty(inputValue))
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
        public void SearchText(string fullUrl, IBasicWebScrapperSite webScrapperImplementation, ConstsBookScrapper consts)
        {
            string fullText = webScrapperImplementation.FullUrlToReadFrom;
            var web = new HtmlWeb();
            var doc = web.Load(fullText);
            //do constów można dodać magic stringi
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "row touchwrap", "class", "div");
            var LinkNodes = webScrapperImplementation.GetAllDescendant(BooksNodes, "div");
            LinkNodes = webScrapperImplementation.GetDescendantsWhereAttributeContains(LinkNodes, "div","class","product-box");
            LinkNodes = webScrapperImplementation.GetDescandant(LinkNodes, "a", 1);
            BooksNodes = webScrapperImplementation.GetAllDescendant(BooksNodes, "h6");
            Books = webScrapperImplementation.GetNamesFromNodes(BooksNodes);
            List<HtmlNode> nodePricesValues = webScrapperImplementation.AllNodes(doc, "cena-box", "class", "div");
            nodePricesValues = webScrapperImplementation.GetFirstDescendant(nodePricesValues, "p", "span", "strike");
            Prices = webScrapperImplementation.GetNamesFromNodes(nodePricesValues);
            var AuthorsNodes = webScrapperImplementation.AllNodes(doc, "desc", "class", "div");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "p");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "a");
            this.Authors = webScrapperImplementation.GetNamesFromNodes(AuthorsNodes);
            this.Links = webScrapperImplementation.GetStringFromAttribute(LinkNodes, "href");
        }
    }
}
