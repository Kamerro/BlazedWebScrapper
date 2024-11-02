using HtmlAgilityPack;
using System.Text;

namespace BlazedWebScrapper.Data
{
    public class PWNSearcher : ISearcherBooks
    {
        public List<string> BooksNames { get; set; }
        public List<string> PricePerBook { get; set; }
        public List<string> AuthorName { get; set; }
        public string BuildFullUrlToSearch(Query query, string inputValue, string authorName, string title)
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
            return $"{query.UrlWithSiteName}{query.ObjectOfInterest}";
        }
        public void SearchText(string fullUrl, IBasicWebScrapperSite webScrapperImplementation, ConstsBookScrapper consts)
        {
            string fullText = webScrapperImplementation.FullUrlToReadFrom;
            var web = new HtmlWeb();
            var doc = web.Load(fullText);
            //do constów można dodać magic stringi
            var Books = webScrapperImplementation.AllNodes(doc, "", "data-productname", "div");
            BooksNames = webScrapperImplementation.GetStringFromAttribute(Books.Select(x => x.Attributes["data-productname"].Value).ToList());
            List<HtmlNode> nodePricesValues = webScrapperImplementation.AllNodes(doc, "", "data-productprice", "div");
            PricePerBook = webScrapperImplementation.GetStringFromAttribute(nodePricesValues.Select(x => x.Attributes["data-productprice"].Value).ToList());
            var Authors = webScrapperImplementation.AllNodes(doc, "emp-info-authors", "class", "div");
            var nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(Authors, "span");    
            nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(Authors, "a");
            AuthorName = webScrapperImplementation.GetNamesFromNodes(nodeAuthorValue).Where((element, index) => index % 2 == 0)
            .ToList(); ;

        }
    }
}
