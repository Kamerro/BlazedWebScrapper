using BlazedWebScrapper.Data.Classes.Configuration;
using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class FilteringOptions
    {
        [Parameter]
        public FilteringSpecification FSpec { get; set; }

        public void DataChanged()
        {
            FSpec.isFilteringAvailable = !FSpec.isFilteringAvailable;
            DataChangedTriggerFlow.InvokeAsync();
        }
        public void SaveChanges()
        {
            FSpec.MaxPrice = isCheckedMaxPrice ? int.Parse(MaxPrice) : 0;
            FSpec.MaxResults = isCheckedMaxResults ? int.Parse(MaxBooksFromSite) : 0;
            DataChangedTriggerFlow.InvokeAsync();
        }
        public void ChangePriceFiltering()
        {
            isCheckedMaxPrice = !isCheckedMaxPrice;
        }

        public void ChangePrice(string value)
        {
            MaxPrice = value;
        }
        [Parameter]
        public EventCallback DataChangedTriggerFlow { get; set; }

        bool isCheckedMaxResults = false;
        bool isCheckedMaxPrice = false;
        private int _intMaxResults = 1;
        private int _intMaxPrice = 1;
        public string MaxBooksFromSite
        {
            get => _intMaxResults.ToString();
            set
            {
                if (int.TryParse(value, out int intValue)) _intMaxResults = intValue;
                else
                {
                    _intMaxResults = 1;
                }
            }
        }
        public string MaxPrice
        {
            get => _intMaxPrice.ToString();
            set
            {
                if (int.TryParse(value, out int intValue)) _intMaxPrice = intValue;
                else
                {
                    _intMaxPrice = 1;
                }
            }
        }
    }
}
