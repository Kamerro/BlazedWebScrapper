using BlazedWebScrapper.Data.Classes.Services;
using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class SearchResult
    {
        [Parameter]
        public BookService BookService { get; set; }

        private bool isSortHaveToBeShow;
    }
}
