﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Inlupp.Data;

namespace Web_Inlupp.ViewModel
{
    public class ProductNewViewModel
    {
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }

        public List<SelectListItem> AllCategories { get; set; } = new List<SelectListItem>();
        public int SelectCategoryId { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Range(1, 1000000), Required]
        public decimal Price { get; set; }
    }
}