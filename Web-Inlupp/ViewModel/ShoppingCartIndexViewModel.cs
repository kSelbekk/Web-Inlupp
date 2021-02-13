using System.Collections.Generic;
using Web_Inlupp.Data;

namespace Web_Inlupp.ViewModel
{
    public class ShoppingCartIndexViewModel
    {
        public List<ShoppingCartViewModel> ShoppingCartViewModels { get; set; }

        public class ShoppingCartViewModel
        {
            public int Id { get; set; }
            public Product Product { get; set; }
            public decimal Price { get; set; }
            public decimal TotalPrice { get; set; }
            public uint Quantity { get; set; }
        }
    }
}