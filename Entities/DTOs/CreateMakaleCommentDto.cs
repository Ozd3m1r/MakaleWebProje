using System.ComponentModel.DataAnnotations;

public class CreateMakaleCommentDto
{
    [Required(ErrorMessage = "Yorum alanı boş bırakılamaz")]
    [MinLength(2, ErrorMessage = "Yorum en az 2 karakter olmalıdır")]
    [MaxLength(500, ErrorMessage = "Yorum 500 karakterden uzun olamaz")]
    public string Comment { get; set; }

    public int MakaleId { get; set; }
    public int UserId { get; set; }
} 