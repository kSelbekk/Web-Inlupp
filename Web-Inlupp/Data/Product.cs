﻿namespace Web_Inlupp.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}