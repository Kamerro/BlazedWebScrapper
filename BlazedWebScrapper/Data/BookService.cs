namespace BlazedWebScrapper.Data
{
    public class BookService
    {
        public void SetAuthor(Book searchBook,string author)
        {
            searchBook.Author.Name = author;
        }
        public void SetTitle(Book searchBook, string title)
        {
            searchBook.Title = title;
        }
    }
}
