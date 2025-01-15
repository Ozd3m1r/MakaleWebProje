using System.ComponentModel.DataAnnotations;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Mevcut şifrenizi girmeniz gerekmektedir.")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Yeni şifrenizi girmeniz gerekmektedir.")]
    [MinLength(6, ErrorMessage = "Yeni şifre en az 6 karakter olmalıdır.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Yeni şifrenizi doğrulamanız gerekmektedir.")]
    [Compare("NewPassword", ErrorMessage = "Yeni şifreler birbirleriyle eşleşmiyor.")]
    public string ConfirmPassword { get; set; }
}
