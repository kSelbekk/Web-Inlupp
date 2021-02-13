namespace Web_Inlupp.Data
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public uint Quantity { get; set; }
    }
}