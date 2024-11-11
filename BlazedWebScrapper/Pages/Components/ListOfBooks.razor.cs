using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class ListOfBooks
    {
        [Parameter]
        public List<Tuple<string, decimal, string>> FullListOfBooks { get; set; } = new List<Tuple<string, decimal, string>>();

        [Parameter]
        public bool IsSortHaveToBeShow { get; set; }
    }
}
