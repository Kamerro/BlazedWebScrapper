using HtmlAgilityPack;

namespace BlazedWebScrapper.Data.Interfaces
{
    public interface INodeReceiver
    {
        public HtmlNode GetNearEndDescandant(List<HtmlNode> nodes, string htmlTag);
        public HtmlNode GetFirstDescendantFromSingleNode(HtmlNode paginationNode, string v);
    }
}