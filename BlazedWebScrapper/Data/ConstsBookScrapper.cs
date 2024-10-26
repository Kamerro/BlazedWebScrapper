using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace BlazedWebScrapper.Data
{
    public class ConstsBookScrapper
    {
        const string productWrapper = "product s-grid-3 product-main-wrap";
        const string aTag = "a";
        const string divTag = "div";
        const string classAttribute = "class";
        public string ProductWrapper { get => productWrapper;}
        public string ATag { get => aTag; }
        public string DivTag { get => divTag; }
        public string ClassAttribute { get => classAttribute; }
    }
}
