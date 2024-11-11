namespace BlazedWebScrapper.Data.Classes.Services
{
    internal class LinkService
    {
        private List<string> _links;
        private string _Base;

        public LinkService(List<string> links, string baseSite)
        {
            _links = links;
            _Base = baseSite;
        }

        public void GenerateLinks()
        {
            for (int i = 0; i < _links.Count; i++)
            {
                _links[i] = _Base + _links[i];
            }
        }
    }
}