namespace BlazedWebScrapper.Data.Classes.Data
{
    public class Book
    {
        public Author Author { get => _author; set { _author = value; } }
        public string Title { get => _title; set { _title = value; } }
        public string Description { get => _description; set { _description = value; } }
        public string Url { get => _url; set { _url = value; } }
        public DateTime DateOfRelease { get => _dateOfRelease; set { _dateOfRelease = value; } }
        private Author _author;
        private string _title;
        private string _description;
        private string _url;
        private DateTime _dateOfRelease;
        public Book()
        {
            _author = new Author();
            _dateOfRelease = new DateTime();
        }
    }
}
