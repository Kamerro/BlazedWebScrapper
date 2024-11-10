using BlazedWebScrapper.Data.Classes.Services;
using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class Header
    {
        [Parameter]
        public EventCallback<ChangeEventArgs> OnInputEvent { get; set; }
        [Parameter]
        public EventCallback<string> SearchPredefinedBook { get; set; }
        [Parameter]
        public string InputValue { get; set; }
        [Parameter]
        public string AuthorName { get; set; }
        [Parameter]
        public string Title { get; set; }

    }
}
