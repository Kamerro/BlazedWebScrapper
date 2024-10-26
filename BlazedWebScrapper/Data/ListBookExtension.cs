namespace BlazedWebScrapper.Data
{
    public static class ListBookExtension
    {
        public static List<Book> InitializeBooks(this List<Book> books)
        {
            books.Add(new Book() { Author = new Author() { Name = "Gabriel García Márquez" }, Title = "Sto lat samotności" });
            books.Add(new Book() { Author = new Author() { Name = "George Orwell" }, Title = "Rok 1984" });
            books.Add(new Book() { Author = new Author() { Name = "J.R.R. Tolkien" }, Title = "Władca Pierścieni: Drużyna Pierścienia" });
            books.Add(new Book() { Author = new Author() { Name = "Haruki Murakami" }, Title = "Kafka nad morzem" });
            books.Add(new Book() { Author = new Author() { Name = "Margaret Atwood" }, Title = "Opowieść Podręcznej" });

            return books.ToList();
        }
    }
}
