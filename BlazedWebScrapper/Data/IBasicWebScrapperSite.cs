using HtmlAgilityPack;

namespace BlazedWebScrapper.Data
{
    public interface IBasicWebScrapperSite
    {
        public string SiteName { get; set; }
        public bool IsSiteNameValid(string _siteName);
        public List<HtmlNode> AllNodes(HtmlDocument doc, string name, string parameterType, string htmlTag);
        public List<string> GetNamesFromNodes(List<HtmlNode> nodes);
        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes,string htmlTag);
    }
}
