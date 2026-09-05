using System.ComponentModel.DataAnnotations;
namespace DevBlog.Business.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Kullanıcı adı veya e-posta alanı zorunludur.")]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool? RememberMe { get; set; }
    }
}
