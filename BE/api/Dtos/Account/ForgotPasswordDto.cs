using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Account
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
    }

    public class VerifyOtpDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string OtpCode { get; set; } = string.Empty;
    }

    public class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string OtpCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        public string NewPassword { get; set; } = string.Empty;
    }
}