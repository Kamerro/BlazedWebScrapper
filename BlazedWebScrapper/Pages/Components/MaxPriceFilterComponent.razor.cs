using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class MaxPriceFilterComponent
    {

        [Parameter]
        public EventCallback TriggerMaxPriceChange { get; set; }

        [Parameter]
        public EventCallback<string> TriggerMaxPrice { get; set; }

        private void ChangeStatusAndInvokeMethod()
        {
            TriggerMaxPriceChange.InvokeAsync();
            StateHasChanged();
        }

        private void ChangeStatusPriceAndInvokeMethod()
        {
            TriggerMaxPrice.InvokeAsync();
            StateHasChanged();
        }
        private bool isCheckedMaxPrice { get; set; }
        public string MaxPrice
        {
            get => maxPrice;
            set
            {
                TriggerMaxPrice.InvokeAsync(value); maxPrice = value;
            }
        }
        private string maxPrice = "1";
    }
}
