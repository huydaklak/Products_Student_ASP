using System.ComponentModel.DataAnnotations;

namespace My_Web.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        [RegularExpression("Admin|User", ErrorMessage = "Vai trò phải là Admin hoặc User")]
        public string Role { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
