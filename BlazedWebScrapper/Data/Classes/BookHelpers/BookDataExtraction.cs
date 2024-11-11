using HtmlAgilityPack;
using BlazedWebScrapper.Data.Interfaces;
using BlazedWebScrapper.Data.Classes.Data;
using BlazedWebScrapper.Data.Classes.Extension_methods;

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
        private string site;
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
            if (pagination.Count > 0)
            {
                var paginationNode = webScrapperImplementation.GetNearEndDescandant(pagination, "li");
                paginationNode = webScrapperImplementation.GetFirstDescendantFromSingleNode(paginationNode, "a");
                return int.Parse(webScrapperImplementation.GetNameFromNode(paginationNode));

            }
            return 1;
        }
        public int GetPaginationCzytelnik(HtmlDocument doc)
        {

            var pagination = webScrapperImplementation.AllNodes(doc, "paginator", "class", "ul");
            if (pagination.Count > 0)
            {
                var paginationNode = webScrapperImplementation.GetNearEndDescandant(pagination, "li");
                paginationNode = webScrapperImplementation.GetFirstDescendantFromSingleNode(paginationNode, "a");
                return int.Parse(webScrapperImplementation.GetNameFromNode(paginationNode));
            }
            return 1;
        }

        internal List<string> ExtractBooksCzytelnik(HtmlDocument doc)
        {
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "productname", "class", "span");
            return webScrapperImplementation.GetNamesFromNodes(BooksNodes);
        }

        internal List<string> ExtractPricesCzytelnik(HtmlDocument doc)
        {
            List<HtmlNode> nodePricesDiv = webScrapperImplementation.AllNodes(doc, "price f-row", "class", "div");
            List<HtmlNode> nodePricesValue = webScrapperImplementation.GetFirstDescendant(nodePricesDiv, "em");
            return webScrapperImplementation.GetNamesFromNodes(nodePricesValue);
        }

        internal List<string> ExtractAuthorsCzytelnik(HtmlDocument doc)
        {
            var AuthorNameNodes = webScrapperImplementation.AllNodes(doc, "brand", "class", "a");
            var Authors = webScrapperImplementation.GetNamesFromNodes(AuthorNameNodes);
            return Authors.LeaveOnlyAuthorName();
        }

        internal List<string> ExtractLinksCzytelnik(HtmlDocument doc)
        {
            var linkNode = webScrapperImplementation.AllNodes(doc, "prodname f-row", "class", "a");
            return webScrapperImplementation.GetStringFromAttribute(linkNode, "href");
        }

        internal List<string> ExtractBooksWN(HtmlDocument doc)
        {
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "row touchwrap", "class", "div");
            BooksNodes = webScrapperImplementation.GetAllDescendant(BooksNodes, "h6");
            return webScrapperImplementation.GetNamesFromNodes(BooksNodes);
        }

        internal List<string> ExtractPricesWN(HtmlDocument doc)
        {
            List<HtmlNode> nodePricesValues = webScrapperImplementation.AllNodes(doc, "cena-box", "class", "div");
            nodePricesValues = webScrapperImplementation.GetFirstDescendant(nodePricesValues, "p", "span", "strike");
            return webScrapperImplementation.GetNamesFromNodes(nodePricesValues);
        }

        internal List<string> ExtractAuthorsWN(HtmlDocument doc)
        {
            var AuthorsNodes = webScrapperImplementation.AllNodes(doc, "desc", "class", "div");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "p");
            AuthorsNodes = webScrapperImplementation.GetFirstDescendant(AuthorsNodes, "a");
            return webScrapperImplementation.GetNamesFromNodes(AuthorsNodes);
        }

        internal List<string> ExtractLinksWN(HtmlDocument doc)
        {
            var LinkNodes = webScrapperImplementation.AllNodes(doc, "row touchwrap", "class", "div");
            LinkNodes = webScrapperImplementation.GetDescendantsWhereAttributeContains(LinkNodes, "div", "class", "product-box");
            LinkNodes = webScrapperImplementation.GetDescandant(LinkNodes, "a", 1);
            return webScrapperImplementation.GetStringFromAttribute(LinkNodes, "href");
        }

        internal int GetPaginationWN(HtmlDocument doc)
        {
            var pagination = webScrapperImplementation.AllNodes(doc, "page last ", "class", "a");
            if (pagination is not null && pagination.Count==1)
            {
                return int.Parse(webScrapperImplementation.GetNameFromNode(pagination[0]));
            }
            return 1;
        }

        internal List<string> ExtractBooksNK(HtmlDocument doc)
        {
            var BooksNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            BooksNodes = webScrapperImplementation.GetFirstDescendant(BooksNodes, "h2");
            BooksNodes = webScrapperImplementation.GetFirstDescendant(BooksNodes, "a");
            return webScrapperImplementation.GetNamesFromNodes(BooksNodes);
        }

        internal List<string> ExtractPricesNK(HtmlDocument doc)
        {
            List<HtmlNode> nodePricesDiv = webScrapperImplementation.AllNodes(doc, "price", "class", "section");
            List<HtmlNode> nodePricesValue = webScrapperImplementation.GetDescandant(nodePricesDiv, "span", 1);
            if (nodePricesValue.Count == 0)
            {
                nodePricesValue = webScrapperImplementation.GetDescandant(nodePricesDiv, "span", 0);
            }
            return webScrapperImplementation.GetNamesFromNodes(nodePricesValue);
        }

        internal List<string> ExtractAuthorsNK(HtmlDocument doc)
        {
            var AuthorNameNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "h4");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "span");
            AuthorNameNodes = webScrapperImplementation.GetFirstDescendant(AuthorNameNodes, "a");
            var Authors = webScrapperImplementation.GetNamesFromNodes(AuthorNameNodes);
            return Authors.LeaveOnlyAuthorName();
        }

        internal List<string> ExtractLinksNK(HtmlDocument doc)
        {
            var LinkNodes = webScrapperImplementation.AllNodes(doc, "fixed", "class", "article");
            LinkNodes = webScrapperImplementation.GetFirstDescendant(LinkNodes, "h2");
            LinkNodes = webScrapperImplementation.GetFirstDescendant(LinkNodes, "a");
            return webScrapperImplementation.GetStringFromAttribute(LinkNodes, "href");
        }

        internal int GetPaginationNK(HtmlDocument doc)
        {
            var pagination = webScrapperImplementation.AllNodes(doc, "formListPage", "name", "form");
            pagination = webScrapperImplementation.GetFirstDescendant(pagination, "strong");
            if (pagination is not null && pagination.Count == 1)
            {
                return int.Parse(webScrapperImplementation.GetNameFromNode(pagination[0]));
            }
            return 1;
        }
    }
}
