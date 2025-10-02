using System.ComponentModel.DataAnnotations.Schema;

namespace My_Web.Models
{
    [Table("products")]
    public class Product
    {
        public int ProductID { get; set; }
        public int? CategoryID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? BrandID { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Brand? Brand { get; set; }
        public Category? Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
