namespace My_Web.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int BrandID { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Brand? Brand { get; set; }
        public Category? Category { get; set; }
    }
}
