using System.Text;

namespace BlazedWebScrapper.Data.Classes.Searchers
{
    internal class BookQueryBuiulder
    {
        internal string BuildObjectOfInterest(string inputValue, string authorName, string title)
        {

            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(inputValue))
            {
                sb.Append(authorName);
                sb.Append(" ");
                sb.Append(title);
            }
            else
            {
                sb.Append(inputValue);
            }
            return sb.ToString();
        }
    }
}