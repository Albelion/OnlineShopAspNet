using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models.ViewModels
{
    public class ProductsCount
    {
        public Products Product { get; set; }
        [Range(0,100)]
        public int Count { get; set; }
    }
}
