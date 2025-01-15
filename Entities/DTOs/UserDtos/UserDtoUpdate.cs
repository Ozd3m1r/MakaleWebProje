public class UserDtoUpdate
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad alanı zorunludur")]
    [MinLength(2, ErrorMessage = "Ad en az 2 karakter olmalıdır")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyad alanı zorunludur")]
    [MinLength(2, ErrorMessage = "Soyad en az 2 karakter olmalıdır")]
    public string SurName { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
    [MinLength(4, ErrorMessage = "Kullanıcı adı en az 4 karakter olmalıdır")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email alanı zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
    public string Email { get; set; }

    public int UserRoleId { get; set; }
} 