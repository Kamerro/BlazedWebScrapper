using HtmlAgilityPack;
using BlazedWebScrapper.Data.Interfaces;

namespace BlazedWebScrapper.Data.Classes.BookHelpers
{
    internal class BookDataExtraction
    {
        private IBasicWebScrapperSite webScrapperImplementation;
        public BookDataExtraction(string site, IBasicWebScrapperSite webScrapperImplementation)
        {
            this.site = site;
            this.webScrapperImplementation = webScrapperImplementation;
        }

        public List<string> ExtractAuthorsPWN(HtmlDocument doc)
        {
            var AuthorNodes = webScrapperImplementation.AllNodes(doc, "emp-info-authors", "class", "div");
            var nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(AuthorNodes, "span");
            nodeAuthorValue = webScrapperImplementation.GetFirstDescendant(AuthorNodes, "a");
            return webScrapperImplementation.GetNamesFromNodes(nodeAuthorValue).Where((element, index) => index % 2 == 0).ToList();
        }

        public List<string> ExtractDataPWN(HtmlDocument doc, string name, string attribute, string htmlTag)
        {
            var Nodes = webScrapperImplementation.AllNodes(doc, name, attribute, htmlTag);
            return webScrapperImplementation.GetStringFromAttribute(Nodes, attribute);
        }

        public List<string> ExtractLinksPWN(HtmlDocument doc)
        {
            var linkNode = webScrapperImplementation.AllNodes(doc, "item-action-url", "class", "a");
            return webScrapperImplementation.GetStringFromAttribute(linkNode, "href").Where((element, index) => index % 4 == 0).ToList();
        }

        public int GetPaginationPWN(HtmlDocument doc)
        {

            var pagination = webScrapperImplementation.AllNodes(doc, "pagination", "class", "div");
            var paginationNode = webScrapperImplementation.GetNearEndDescandant(pagination, "li");
            paginationNode = webScrapperImplementation.GetFirstDescendantFromSingleNode(paginationNode, "a");
            return int.Parse(webScrapperImplementation.GetNameFromNode(paginationNode));
        }
    }
}
