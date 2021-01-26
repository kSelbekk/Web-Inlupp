using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Inlupp.Data;

namespace Web_Inlupp.ViewModel
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public List<SelectListItem> AllCategories { get; set; } = new List<SelectListItem>();

        public int SelectCategoryId { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Price { get; set; }
    }
}