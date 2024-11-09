namespace BlazedWebScrapper.Data.Classes.Searchers
{
    internal class LinkService
    {
        private List<string> _links;
        private string _PWNBase;

        public LinkService(List<string> links, string pWNBase)
        {
            this._links = links;
            this._PWNBase = pWNBase;
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