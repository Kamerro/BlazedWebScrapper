namespace BlazedWebScrapper.Data
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
        private string _objectOfInterest = String.Empty;

        public string UrlWithSiteName
        {
            get => _urlWithSiteName;
            set
            {
                _urlWithSiteName = value;
            }
        }
        private string _urlWithSiteName = String.Empty;

    }
}
