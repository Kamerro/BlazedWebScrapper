namespace BlazedWebScrapper.Data.Classes.Services
{
    internal class LinkService
    {
        private List<string> _links;
        private string _PWNBase;

        public LinkService(List<string> links, string pWNBase)
        {
            _links = links;
            _PWNBase = pWNBase;
        }

        public void GenerateLinks()
        {
            for (int i = 0; i < _links.Count; i++)
            {
                _links[i] = _PWNBase + _links[i];
            }
        }
    }
}