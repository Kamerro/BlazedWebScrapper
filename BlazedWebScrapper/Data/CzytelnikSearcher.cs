using HtmlAgilityPack;
using System.Text;

namespace BlazedWebScrapper.Data
{
    public class CzytelnikSearcher:ISearcherBooks
    {
        public List<string> Books { get; set; }
        public List<string> Prices { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Links { get; set; }
        public Query query { get; set; }
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }

        public CzytelnikSearcher(Query _query,IBasicWebScrapperSite wsi)
        {
            query = _query;
            webScrapperImplementation = wsi;
        }
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
            webScrapperImplementation.FullUrlToReadFrom = $"{query.UrlWithSiteName}{query.ObjectOfInterest}/1/phot/5?url={query.ObjectOfInterest}";
        }
        public void SearchText(string fullUrl, IBasicWebScrapperSite webScrapperImplementation, ConstsBookScrapper consts)
        {
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
            for(int i = 0; i < Links.Count; i++)
            {
                Links[i] = consts.CzytelnikBase + Links[i];
            }

        }
    }


    public static class ListExtension
    {
        public static List<string> LeaveOnlyAuthorName(this List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace("\n", "").Trim();
            }
            return list;
        }
    }

}
