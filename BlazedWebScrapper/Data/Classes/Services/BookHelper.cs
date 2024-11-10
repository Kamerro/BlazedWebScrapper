using BlazedWebScrapper.Data.Classes.Data;

namespace BlazedWebScrapper.Data.Classes.Services
{
    public class BookHelper
    {
        public void SetAuthor(Book searchBook, string author)
        {
            searchBook.Author.Name = author;
        }
        public void SetTitle(Book searchBook, string title)
        {
            searchBook.Title = title;
        }
    }
}
