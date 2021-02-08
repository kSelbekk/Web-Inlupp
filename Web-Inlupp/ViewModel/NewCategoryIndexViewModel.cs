using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.ViewModel
{
    public class NewCategoryIndexViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Description { get; set; }
    }
}