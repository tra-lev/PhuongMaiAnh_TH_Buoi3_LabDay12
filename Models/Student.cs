using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Pma_Lab03.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, MinimumLength = 4,
            ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$",
            ErrorMessage = "Email phải có đuôi gmail.com")]
        public string? Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).+$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, chữ số và ký tự đặc biệt")]
        public string? Password { get; set; }

        [Required]
        public Branch? Branch { get; set; } 

        [Required]
        public Gender? Gender { get; set; } 

        public bool IsRegular { get; set; } 

        [DataType(DataType.MultilineText)]
        [Required]
        public string? Address { get; set; } 

        [Range(typeof(DateTime), "01/01/1963", "31/12/2005")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBorth { get; set; }

        [Display(Name = "Điểm")]
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải từ 0.0 đến 10.0")]
        public double? Score { get; set; }
    }
}
