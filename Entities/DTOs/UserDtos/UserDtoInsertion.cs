public class UserDtoInsertion
{
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

    [Required(ErrorMessage = "Şifre zorunludur")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Rol seçimi zorunludur")]
    public int UserRoleId { get; set; }
} 