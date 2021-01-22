using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.Data
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Range(1, 1000000)]
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}