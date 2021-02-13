using System.Collections.Generic;
using Web_Inlupp.Data;

namespace Web_Inlupp.ViewModel
{
    public class ShoppingCartAddToCartViewModel
    {
        public List<ShoppingCartViewModel> ProductsInCart { get; set; }

        public class ShoppingCartViewModel
        {
            public int Id { get; set; }
            public Product ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal TotalPrice { get; set; }
            public uint Quantity { get; set; }
        }
    }
}