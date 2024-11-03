using BlazedWebScrapper.Data;
using BlazedWebScrapper.Data.Classes.Configuration;
using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Data;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace BlazedWebScrapper.Pages
{
    public partial class Ksiazki
    {
        [Inject]
        public IBasicWebScrapperSite webScrapperImplementation { get; set; }
        [Inject]
        public IFactorySearcher factorySearcher { get; set; }
        private ISearcherBooks searcherBooksCzytelnik;
        private ISearcherBooks searcherBooksPWN;
        private ISearcherBooks searcherBooksNiezwykle;
        private string inputValue = "";
        private bool isSearchDone = false;
        private TabConfigurator tabConfigurator;
        private ConstsBookScrapper consts;
        private Book searchBook;
        private List<Book> listOfBooks;
        private Query query;
        void SearchPredefinedBook()
        {
            searcherBooksCzytelnik = factorySearcher.GetSearcher("Czytelnik", query, webScrapperImplementation);
            searcherBooksCzytelnik.BuildFullUrlToSearch(inputValue, searchBook.Author.Name, searchBook.Title, consts.CzytelnikSite);
            searcherBooksCzytelnik.SearchText();
            searcherBooksPWN = factorySearcher.GetSearcher("PWN", query, webScrapperImplementation);
            searcherBooksPWN.BuildFullUrlToSearch(inputValue, searchBook.Author.Name, searchBook.Title, consts.PWNSite);
            searcherBooksPWN.SearchText();
            searcherBooksNiezwykle = factorySearcher.GetSearcher("Niezwykle", query, webScrapperImplementation);
            searcherBooksNiezwykle.BuildFullUrlToSearch(inputValue, searchBook.Author.Name, searchBook.Title, consts.NiezwykleSite);
            searcherBooksNiezwykle.SearchText();
            isSearchDone = true;
        }
        protected override void OnInitialized()
        {
            tabConfigurator = new TabConfigurator();
            consts = new ConstsBookScrapper();
            searchBook = new Book();
            listOfBooks = new List<Book>();
            query = new Query();
            tabConfigurator.TileColor = Color.Green.Name;
            listOfBooks.InitializeBooks();
        }
    }
}
