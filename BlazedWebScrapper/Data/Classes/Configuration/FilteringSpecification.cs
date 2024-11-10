namespace BlazedWebScrapper.Data.Classes.Configuration
{
    public class FilteringSpecification
    {
        public bool isFilteringAvailable { get; set; }
        public int MaxPrice { get; set; } = 0;
        public int MaxResults { get; set; } = 0;

    }
}
