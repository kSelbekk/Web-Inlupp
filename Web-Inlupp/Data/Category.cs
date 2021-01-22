using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.Data
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }
    }
}