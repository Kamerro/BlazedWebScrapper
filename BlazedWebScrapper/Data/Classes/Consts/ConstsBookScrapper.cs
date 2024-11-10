
namespace BlazedWebScrapper.Data.Classes.Consts
{
    public class ConstsBookScrapper{ 
         //albatros (ciężkie)
        const string productWrapper = "product s-grid-3 product-main-wrap";
        const string aTag = "a";
        const string divTag = "div";
        const string classAttribute = "class";
        const string czytelnikSite = "https://czytelnik.pl/pl/searchquery/";
        const string pwnSite = "https://ksiegarnia.pwn.pl/szukaj?q=";
        const string czytelnikBase = "https://czytelnik.pl";
        const string pwnBase = "https://ksiegarnia.pwn.pl/";
        const string wydawnictwoNiezwykle = "https://wydawnictwoniezwykle.pl/";
        const string niezwykleSite = "https://wydawnictwoniezwykle.pl/products/search?keyword=";
        const string naszaKsiegarniaBase = "https://nk.com.pl/";
        const string naszaSite = "https://nk.com.pl/szukaj.html?searchText=";

        public string ProductWrapper { get => productWrapper; }
        public string ATag { get => aTag; }
        public string DivTag { get => divTag; }
        public string ClassAttribute { get => classAttribute; }
        public string CzytelnikSite { get => czytelnikSite; }
        public string PWNSite { get => pwnSite; }
        public string CzytelnikBase { get => czytelnikBase; }

        public string PWNBase { get => pwnBase; }
        public string NiezwykleBase { get => wydawnictwoNiezwykle; }
        public string NiezwykleSite { get => niezwykleSite; }
        public string NaszaKsiegarniaBase { get=>naszaKsiegarniaBase;}
        public string NaszaSite { get=>naszaSite;}


    }
}
