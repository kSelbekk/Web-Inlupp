using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Web_Inlupp.ViewModel
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }

        [MaxLength(150), Required]
        public string Description { get; set; }
    }
}