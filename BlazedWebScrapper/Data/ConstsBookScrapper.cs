using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using static System.Net.WebRequestMethods;

namespace BlazedWebScrapper.Data
{
    public class ConstsBookScrapper
    {    //Wydawnictwoniezwykle.
         //ksiegarnia.pwn.pl
         //nk.com.pl
         //albatros (ciężkie)
         //czytelnik.pl/
        const string productWrapper = "product s-grid-3 product-main-wrap";
        const string aTag = "a";
        const string divTag = "div";
        const string classAttribute = "class";
        const string czytelnikSite = "https://czytelnik.pl/pl/searchquery/";
        const string pwnSite = "https://ksiegarnia.pwn.pl/szukaj?q=";
        public string ProductWrapper { get => productWrapper;}
        public string ATag { get => aTag; }
        public string DivTag { get => divTag; }
        public string ClassAttribute { get => classAttribute; }
        public string CzytelnikSite { get => czytelnikSite;}
        public string PWNSite { get => pwnSite; }

    }
}
