using HtmlAgilityPack;

namespace BlazedWebScrapper.Data.Interfaces
{
    public interface IStringReceiver
    {
        public List<string> GetNamesFromNodes(List<HtmlNode> nodes);
        public List<string> GetStringFromAttribute(List<HtmlNode> nodes, string attribute);
        string GetNameFromNode(HtmlNode paginationNode);

    }
}