using BlazedWebScrapper.Data.Classes.Configuration;
using System.Text.RegularExpressions;

namespace BlazedWebScrapper.Data.Classes.Services
{
    public class BookService
    {
        public FilteringSpecification filterSpecification;

        public List<Tuple<string, decimal, string>> FullListOfBooks = new List<Tuple<string, decimal, string>>();

        public List<Tuple<string, decimal, string>> FullListOfBooksPWN = new List<Tuple<string, decimal, string>>();
        public List<Tuple<string, decimal, string>> FullListOfBooksNK = new List<Tuple<string, decimal, string>>();
        public List<Tuple<string, decimal, string>> FullListOfBooksWN = new List<Tuple<string, decimal, string>>();
        public List<Tuple<string, decimal, string>> FullListOfBooksCzytelnik = new List<Tuple<string, decimal, string>>();
        public List<Tuple<string, decimal, string>> SortedListOfBooks = new List<Tuple<string, decimal, string>>();

        internal void GenerateFullListOfBooks(List<string> books, List<string> authors, List<string> links, List<string> prices)
        {
            for (int i = 0; i < books.Count && i < authors.Count && i < links.Count; i++)
            {
                if (filterSpecification.MaxResults == 0 || filterSpecification.MaxResults > FullListOfBooks.Count)
                {
                    var match = Regex.Match(prices[i], @"\d+([.,]\d{1,2})?");
                    if (match.Success)
                    {
                        string filteredValue = match.Value.Replace(".", ",");

                        if (decimal.TryParse(filteredValue, out decimal price))
                        {
                            if (filterSpecification.MaxPrice == 0 || (int)price <= filterSpecification.MaxPrice - 1)
                            {
                                FullListOfBooks.Add(new($"{authors[i]}-{books[i]}", price, links[i]));
                            }
                        }
                    }
                }

            }
            FullListOfBooks.Clear();
            FullListOfBooks = FuseAllOfTheListsToOne();
        }

        private List<Tuple<string, decimal, string>> FuseAllOfTheListsToOne()
        {
            var listToReturn = new List<Tuple<string, decimal, string>>();
            listToReturn.AddRange(FullListOfBooksPWN);
            listToReturn.AddRange(FullListOfBooksNK);
            listToReturn.AddRange(FullListOfBooksWN);
            listToReturn.AddRange(FullListOfBooksCzytelnik);
            return listToReturn;
        }
        internal void GenerateFullListOfBooksForPWN(List<string> books, List<string> authors, List<string> links, List<string> prices)
        {
            for (int i = 0; i < books.Count && i < authors.Count && i < links.Count; i++)
            {
                if (filterSpecification.MaxResults == 0 || filterSpecification.MaxResults > FullListOfBooksPWN.Count)
                {
                    var match = Regex.Match(prices[i], @"\d+([.,]\d{1,2})?");
                    if (match.Success)
                    {
                        string filteredValue = match.Value.Replace(".", ",");

                        if (decimal.TryParse(filteredValue, out decimal price))
                        {
                            if (filterSpecification.MaxPrice == 0 || (int)price <= filterSpecification.MaxPrice - 1)
                            {
                                FullListOfBooksPWN.Add(new($"{authors[i]}-{books[i]}", price, links[i]));
                            }
                        }
                    }
                }

            }
            FullListOfBooks.Clear();
            FullListOfBooks = FuseAllOfTheListsToOne();
        }
        internal void GenerateFullListOfBooksForWN(List<string> books, List<string> authors, List<string> links, List<string> prices)
        {
            for (int i = 0; i < books.Count && i < authors.Count && i < links.Count; i++)
            {
                if (filterSpecification.MaxResults == 0 || filterSpecification.MaxResults > FullListOfBooksWN.Count)
                {
                    var match = Regex.Match(prices[i], @"\d+([.,]\d{1,2})?");
                    if (match.Success)
                    {
                        string filteredValue = match.Value.Replace(".", ",");

                        if (decimal.TryParse(filteredValue, out decimal price))
                        {
                            if (filterSpecification.MaxPrice == 0 || (int)price <= filterSpecification.MaxPrice - 1)
                            {
                                FullListOfBooksWN.Add(new($"{authors[i]}-{books[i]}", price, links[i]));
                            }
                        }
                    }
                }

            }
            FullListOfBooks.Clear();
            FullListOfBooks = FuseAllOfTheListsToOne();
        }
        internal void GenerateFullListOfBooksForNK(List<string> books, List<string> authors, List<string> links, List<string> prices)
        {
            for (int i = 0; i < books.Count && i < authors.Count && i < links.Count; i++)
            {
                if (filterSpecification.MaxResults == 0 || filterSpecification.MaxResults > FullListOfBooksNK.Count)
                {
                    var match = Regex.Match(prices[i], @"\d+([.,]\d{1,2})?");
                    if (match.Success)
                    {
                        string filteredValue = match.Value.Replace(".", ",");

                        if (decimal.TryParse(filteredValue, out decimal price))
                        {
                            if (filterSpecification.MaxPrice == 0 || (int)price <= filterSpecification.MaxPrice - 1)
                            {
                                FullListOfBooksNK.Add(new($"{authors[i]}-{books[i]}", price, links[i]));
                            }
                        }
                    }
                }

            }
            FullListOfBooks.Clear();
            FullListOfBooks = FuseAllOfTheListsToOne();
        }
        internal void GenerateFullListOfBooksForCzytelnik(List<string> books, List<string> authors, List<string> links, List<string> prices)
        {
            for (int i = 0; i < books.Count && i < authors.Count && i < links.Count; i++)
            {
                if (filterSpecification.MaxResults == 0 || filterSpecification.MaxResults > FullListOfBooksCzytelnik.Count)
                {
                    var match = Regex.Match(prices[i], @"\d+([.,]\d{1,2})?");
                    if (match.Success)
                    {
                        string filteredValue = match.Value.Replace(".", ",");

                        if (decimal.TryParse(filteredValue, out decimal price))
                        {
                            if (filterSpecification.MaxPrice == 0 || (int)price <= filterSpecification.MaxPrice - 1)
                            {
                                FullListOfBooksCzytelnik.Add(new($"{authors[i]}-{books[i]}", price, links[i]));
                            }
                        }
                    }
                }

            }
            FullListOfBooks.Clear();
            FullListOfBooks = FuseAllOfTheListsToOne();
        }

    }
}
