using HtmlAgilityPack;
using System;
using System.Linq.Expressions;
using static System.Reflection.Metadata.BlobBuilder;

namespace BlazedWebScrapper.Data
{
    public abstract class BasicWebScrapperSite : IBasicWebScrapperSite
    {
        public string SiteName
        {
            get => _siteName;
            set
            {
                if (IsSiteNameValid(value))
                {

                    _siteName = value;
                }
                else
                {
                    throw new InvalidOperationException("Invalid site name not valid URI");
                }
            }
        }
        protected string _siteName;
        public bool IsSiteNameValid(string siteName)
        {
            return Uri.IsWellFormedUriString(siteName, UriKind.Absolute);
        }

        public List<HtmlNode> AllNodes(HtmlDocument doc, string name, string parameterType, string htmlTag)
        {
            if (doc is null || doc.DocumentNode is null)
            {
                throw new ArgumentNullException($"{nameof(doc)} is null or {nameof(doc.DocumentNode)} is null");
            }
            var nodes = doc.DocumentNode.Descendants(htmlTag)?.Where(x =>
            x.Attributes[parameterType] is not null && x.Attributes[parameterType].Value == name
            ).ToList();

            return nodes!;
        }

        public List<string> GetNamesFromNodes(List<HtmlNode> nodes)
        {
            List<string> names = new List<string>();
            nodes.ForEach(x => names.Add(x.InnerHtml));
            return names;
        }

        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes,string htmlTag)
        {
            List<HtmlNode> innerNodes = new List<HtmlNode>();

            nodes.ForEach(x => innerNodes.Add(x.Descendants(htmlTag).First()));
            return innerNodes;
        }
    }
}
