using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Inlupp.Data;

namespace Web_Inlupp.ViewModel
{
    public class ProductIndexViewModel
    {
        [MaxLength(50)]
        public string q { get; set; }

        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

        public List<SelectListItem> SortingList { get; set; } = new List<SelectListItem>();
        public int SelectSort { get; set; }
        public string SearchOption { get; set; }


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