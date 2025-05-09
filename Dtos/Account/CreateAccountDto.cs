using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class CreateAccountDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string PassWord { get; set; } = string.Empty;

        public string? Role { get; set; }
    }
}