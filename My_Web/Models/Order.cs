using System.ComponentModel.DataAnnotations;

namespace My_Web.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public string Status { get; set; } = "pending"; // chuẩn hóa mã trạng thái

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public bool CanCancel => Status == "pending" || Status == "processing";
    }
}
