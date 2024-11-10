using BlazedWebScrapper.Data.Classes.Configuration;
using System.Text.RegularExpressions;

namespace BlazedWebScrapper.Data.Classes.Services
{
    public class BookService
    {
        public FilteringSpecification filterSpecification;
        public List<Tuple<string,decimal,string>> FullListOfBooks = new List<Tuple<string, decimal, string>>();

        internal void GenerateFullListOfBooks(List<string> books, List<string> authors, List<string> links,List<string> prices)
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
                            if (filterSpecification.MaxPrice == 0 || (int)price <= filterSpecification.MaxPrice-1)
                            {
                                FullListOfBooks.Add(new($"{authors[i]}-{books[i]}", price, links[i]));
                            }
                        }
                    }
                }

            }
        }
    }
}
