using HtmlAgilityPack;

namespace BlazedWebScrapper.Data
{
    public interface IBasicWebScrapperSite
    {
        public string FullUrlToReadFrom { get; set; }
        public bool IsSiteNameValid(string _siteName);
        public List<HtmlNode> AllNodes(HtmlDocument doc, string name, string parameterType, string htmlTag);
        public List<string> GetNamesFromNodes(List<HtmlNode> nodes);
        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes,string htmlTag);
        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes, string htmlTag, string htmlTagAlt,string htmlTagAlt_sec);

        public List<string> GetStringFromAttribute(List<HtmlNode> nodes,string attribute);
        public List<HtmlNode> GetAllDescendant(List<HtmlNode> nodes, string htmlTag);
        public List<HtmlNode> GetDescandant(List<HtmlNode> nodes, string htmlTag,int desc);
        public List<HtmlNode> GetDescendantsWhereAttributeContains(List<HtmlNode> nodes, string htmlTag, string attribute, string substring);


    }
}
