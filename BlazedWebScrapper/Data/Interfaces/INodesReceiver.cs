using HtmlAgilityPack;

namespace BlazedWebScrapper.Data.Interfaces
{
    public interface INodesReceiver
    {
        public List<HtmlNode> AllNodes(HtmlDocument doc, string name, string parameterType, string htmlTag);

        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes, string htmlTag);
        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes, string htmlTag, string htmlTagAlt, string htmlTagAlt_sec);
        public List<HtmlNode> GetAllDescendant(List<HtmlNode> nodes, string htmlTag);
        public List<HtmlNode> GetDescandant(List<HtmlNode> nodes, string htmlTag, int desc);
        public List<HtmlNode> GetDescendantsWhereAttributeContains(List<HtmlNode> nodes, string htmlTag, string attribute, string substring);
    }
}