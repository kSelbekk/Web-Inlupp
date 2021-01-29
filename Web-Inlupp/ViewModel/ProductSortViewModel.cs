using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_Inlupp.ViewModel
{
    public class ProductSortViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public List<ProductIndexViewModel.ProductViewModel> Products { get; set; } = new List<ProductIndexViewModel.ProductViewModel>();

        public List<SelectListItem> SortingList { get; set; } = new List<SelectListItem>();
        public int SelectSort { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Price { get; set; }
    }
}