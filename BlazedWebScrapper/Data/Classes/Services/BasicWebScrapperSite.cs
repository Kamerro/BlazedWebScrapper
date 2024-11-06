using BlazedWebScrapper.Data.Interfaces;
using HtmlAgilityPack;

namespace BlazedWebScrapper.Data.Classes.Services
{
    public abstract class BasicWebScrapperSite : IBasicWebScrapperSite
    {
        public string FullUrlToReadFrom
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
            List<HtmlNode> nodes = new List<HtmlNode>();
            if (name == "")
            {
                nodes = doc.DocumentNode.Descendants(htmlTag)?.Where(x =>
                x.Attributes[parameterType] is not null && x.Attributes[parameterType].Value.Contains(name)
                ).ToList();
            }
            else
            {
                nodes = doc.DocumentNode.Descendants(htmlTag)?.Where(x =>
               x.Attributes[parameterType] is not null && x.Attributes[parameterType].Value == name
               ).ToList();
            }
            return nodes!;
        }

        public List<string> GetNamesFromNodes(List<HtmlNode> nodes)
        {
            List<string> names = new List<string>();
            nodes.ForEach(x => names.Add(x?.InnerHtml));
            return names;
        }

        public List<string> GetStringFromAttribute(List<HtmlNode> nodes, string attribute)
        {
            return nodes.Select(x => x.Attributes[attribute]?.Value).ToList();
        }

        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes, string htmlTag)
        {
            List<HtmlNode> innerNodes = new List<HtmlNode>();

            nodes.ForEach(x => { if (x is not null) innerNodes.Add(x.Descendants(htmlTag).FirstOrDefault()); });
            return innerNodes;
        }
        public List<HtmlNode> GetDescandant(List<HtmlNode> nodes, string htmlTag, int number)
        {
            List<HtmlNode> innerNodes = new List<HtmlNode>();
            nodes.ForEach(x => { if (x is not null) innerNodes.Add(x.Descendants(htmlTag)?.ToList()[number]); });
            return innerNodes;
        }
        public List<HtmlNode> GetDescendantsWhereAttributeContains(List<HtmlNode> nodes, string htmlTag, string attribute, string substring)
        {
            List<HtmlNode> innerNodes = new List<HtmlNode>();

            nodes.ForEach(node =>
            {
                if (node != null)
                {
                    innerNodes.AddRange(node
                        .Descendants(htmlTag)
                        .Where(descendant =>
                            descendant.Attributes.Contains(attribute) &&
                            descendant.Attributes[attribute].Value.Contains(substring)));
                }
            });

            return innerNodes;
        }
        public List<HtmlNode> GetAllDescendant(List<HtmlNode> nodes, string htmlTag)
        {
            List<HtmlNode> innerNodes = new List<HtmlNode>();

            nodes.ForEach(x => innerNodes.AddRange(x.Descendants(htmlTag)));
            return innerNodes;
        }

        public List<HtmlNode> GetFirstDescendant(List<HtmlNode> nodes, string htmlTag, string htmlTagAlt, string htmlTagAltSec)
        {
            List<HtmlNode> innerNodes = new List<HtmlNode>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i].Descendants(htmlTag).FirstOrDefault();
                if (node is not null)
                {
                    node = nodes[i].Descendants(htmlTagAlt).Skip(1).First();
                    if (node is not null)
                        innerNodes.Add(node);
                    else
                    {
                        node = nodes[i].Descendants(htmlTagAltSec).FirstOrDefault();
                        if (node is not null)
                            innerNodes.Add(node);
                    }
                }
                else
                {
                    node = nodes[i].Descendants(htmlTagAlt).FirstOrDefault();
                    if (node is not null)
                        innerNodes.Add(node);
                }
            }
            return innerNodes;
        }
    }
}
