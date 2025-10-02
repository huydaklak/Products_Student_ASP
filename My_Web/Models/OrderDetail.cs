using System.ComponentModel.DataAnnotations.Schema;

namespace My_Web.Models
{
    [Table("orderdetails")]
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }

        // Foreign keys
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        // Data
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
