using HtmlAgilityPack;

namespace BlazedWebScrapper.Data.Interfaces
{
    public interface IBasicWebScrapperSite:IStringReceiver,INodesReceiver,INodeReceiver
    {
        public string FullUrlToReadFrom { get; set; }
        public bool IsSiteNameValid(string _siteName);
    }
}
