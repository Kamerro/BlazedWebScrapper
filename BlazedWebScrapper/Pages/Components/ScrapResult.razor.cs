using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class ScrapResult
    {
        [Parameter]
        public RenderFragment Result { get; set; }
        [Parameter]
        public RenderFragment NotFound { get; set; }
        [Parameter]
        public List<string> BooksNames { get; set; }
        [Parameter]
        public List<string> Author { get; set; }

    }
}
