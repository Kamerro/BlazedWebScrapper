using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    public class PWNSearcher : ISearcherBooks
    {
        public PWNSearcher(Query _query, IBasicWebScrapperSite wsi, BookServiceList bksrv)
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
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "", "data-productname", "div");
            Books = webScrapperImplementation.GetStringFromAttribute(BooksNodes, "data-productname");
            List<HtmlNode> nodePricesValues = webScrapperImplementation.AllNodes(doc, "", "data-productprice", "div");
            Prices = webScrapperImplementation.GetStringFromAttribute(nodePricesValues, "data-productprice");
            var Authors = webScrapperImplementation.AllNodes(doc, "emp-info-authors", "class", "div");
            var nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(Authors, "span");
            nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(Authors, "a");
            this.Authors = webScrapperImplementation.GetNamesFromNodes(nodeAuthorValue).Where((element, index) => index % 2 == 0)
            .ToList();

            var linkNode = webScrapperImplementation.AllNodes(doc, "item-action-url", "class", "a");
            Links = webScrapperImplementation.GetStringFromAttribute(linkNode, "href").Where((element, index) => index % 4 == 0).ToList();
            for (int i = 0; i < Links.Count; i++)
            {
                Links[i] = consts.PWNBase + Links[i];
            }

            for (int i = 0; i < Books.Count; i++)
            {
                var match = Regex.Match(Prices[i], @"\d+([.,]\d{1,2})?");
                if (match.Success)
                {
                    string filteredValue = match.Value.Replace(".", ",");

                    if (decimal.TryParse(filteredValue, out decimal price))
                    {
                        bookServiceList.FullListOfBooks.Add(new($"{this.Authors[i]}-{Books[i]}",price, Links[i]));
                    }
                }
              
            }
        }
    }
}
