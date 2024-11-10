using BlazedWebScrapper.Data.Classes.Data;
using Microsoft.AspNetCore.Components;

namespace BlazedWebScrapper.Pages.Components
{
    public partial class Table
    {
        [Parameter]
        public List<Book> ListOfBooks { get; set; }

        [Parameter]
        public Book SearchBook { get; set; }

        [Parameter]
        public Action<Book, string> SetAuthor { get; set; }

        [Parameter]
        public Action<Book, string> SetTitle { get; set; }

        public List<string> listOfCellsColorsAuthors = new List<string>();
        public bool isInitialized = false;

        public List<string> InitializeListOfStringsForAuthors()
        {
            return Enumerable.Repeat("Black", ListOfBooks.Count).ToList();
        }

        protected override void OnInitialized()
        {
            if (ListOfBooks != null)
            {
                listOfCellsColorsAuthors = InitializeListOfStringsForAuthors();
                isInitialized = true;
            }
        }
    }
}
