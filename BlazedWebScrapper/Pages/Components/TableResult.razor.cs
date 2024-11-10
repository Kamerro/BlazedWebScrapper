using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class TableResult
    {
        [Parameter]
        public List<string> BooksNames { get; set; }

        [Parameter]
        public List<string> PricePerBook { get; set; }

        [Parameter]
        public List<string> Author { get; set; }

        [Parameter]
        public List<string> Link { get; set; }

        [Parameter]
        public string Source { get; set; }

    }
}
