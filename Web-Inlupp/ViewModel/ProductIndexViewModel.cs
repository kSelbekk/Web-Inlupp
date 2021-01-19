﻿using System.Collections.Generic;
using Web_Inlupp.Data;

namespace Web_Inlupp.ViewModel
{
    public class ProductIndexViewModel
    {
        public List<ProductViewModel> products { get; set; } = new List<ProductViewModel>();

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Category Category { get; set; }

            public string Description { get; set; }
            public decimal Price { get; set; }
        }
    }
}