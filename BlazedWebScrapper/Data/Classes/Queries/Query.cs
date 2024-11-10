namespace BlazedWebScrapper.Data.Classes.Queries
{
    public class Query
    {
        public string ObjectOfInterest
        {
            get => _objectOfInterest;
            set
            {
                _objectOfInterest = Uri.EscapeDataString(value);
            }
        }
        private string _objectOfInterest = string.Empty;

        public string UrlWithSiteName
        {
            get => _urlWithSiteName;
            set
            {
                _urlWithSiteName = value;
            }
        }
        private string _urlWithSiteName = string.Empty;

    }
}
