using BlazedWebScrapper.Data;
using BlazedWebScrapper.Data.Classes.Configuration;
using BlazedWebScrapper.Data.Classes.Consts;
using BlazedWebScrapper.Data.Classes.Data;
using BlazedWebScrapper.Data.Classes.Queries;
using BlazedWebScrapper.Data.Classes.Services;
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

        [Inject]
        public BookServiceList bookServiceList { get; set; }

        private ISearcherBooks searcherBooksCzytelnik;
        private ISearcherBooks searcherBooksPWN;
        private ISearcherBooks searcherBooksNiezwykle;
        private ISearcherBooks searcherBooksNaszaKsiegarnia;

        private bool isSearchDone = default;
        private TabConfigurator tabConfigurator;
        private ConstsBookScrapper consts;
        private Book searchBook;
        private List<Book> listOfBooks;
        private Query query;
        void SearchPredefinedBook(string inputValue)
        {
            searcherBooksNaszaKsiegarnia = factorySearcher.GetSearcher("Nasza", query, webScrapperImplementation,bookServiceList);
            searcherBooksNaszaKsiegarnia.BuildFullUrlToSearch(inputValue, searchBook.Author.Name, searchBook.Title, consts.NaszaSite);
            searcherBooksNaszaKsiegarnia.SearchText();
            searcherBooksCzytelnik = factorySearcher.GetSearcher("Czytelnik", query, webScrapperImplementation, bookServiceList);
            searcherBooksCzytelnik.BuildFullUrlToSearch(inputValue, searchBook.Author.Name, searchBook.Title, consts.CzytelnikSite);
            searcherBooksCzytelnik.SearchText();
            searcherBooksPWN = factorySearcher.GetSearcher("PWN", query, webScrapperImplementation, bookServiceList);
            searcherBooksPWN.BuildFullUrlToSearch(inputValue, searchBook.Author.Name, searchBook.Title, consts.PWNSite);
            searcherBooksPWN.SearchText();
            searcherBooksNiezwykle = factorySearcher.GetSearcher("Niezwykle", query, webScrapperImplementation, bookServiceList);
            searcherBooksNiezwykle.BuildFullUrlToSearch(inputValue, searchBook.Author.Name, searchBook.Title, consts.NiezwykleSite);
            searcherBooksNiezwykle.SearchText();

            bookServiceList.FullListOfBooks = bookServiceList.FullListOfBooks.OrderBy(x => x.Item2).ToList();
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
