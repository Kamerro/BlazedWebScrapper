using HtmlAgilityPack;
using System.Text;

namespace BlazedWebScrapper.Data
{
    public class CzytelnikSearcher
    {
        public List<string> BooksNames { get; set; }
        public List<string> PricePerBook { get; set; }

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
            return $"{query.UrlWithSiteName}{query.ObjectOfInterest}/1/phot/5?url={query.ObjectOfInterest}";
        }
        public void SearchText(string fullUrl, IBasicWebScrapperSite webScrapperImplementation, ConstsBookScrapper consts)
        {
            string fullText = webScrapperImplementation.FullUrlToReadFrom;
            var web = new HtmlWeb();
            var doc = web.Load(fullText);
            var nodesProducts = webScrapperImplementation.AllNodes(doc, consts.ProductWrapper,
                                                                       consts.ClassAttribute,
                                                                       consts.ATag);
            //do constów można dodać magic stringi
            var Books = webScrapperImplementation.AllNodes(doc, "productname", "class", "span");
            BooksNames = webScrapperImplementation.GetNamesFromNodes(Books);
            List<HtmlNode> nodePricesDiv = webScrapperImplementation.AllNodes(doc, "price f-row", "class", "div");
            List<HtmlNode> nodePricesValue = webScrapperImplementation.GetFirstDescendant(nodePricesDiv, "em");

            PricePerBook = webScrapperImplementation.GetNamesFromNodes(nodePricesValue);
        }
    }
}
