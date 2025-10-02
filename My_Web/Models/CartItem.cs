using System.ComponentModel.DataAnnotations.Schema;

namespace My_Web.Models
{
    [Table("cartitems")]
    public class CartItem
    {
        public int CartItemID { get; set; }

        // Foreign keys
        public int ProductID { get; set; }
        public int UserID { get; set; }

        // Data
        public int Quantity { get; set; }

        // Navigation
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
