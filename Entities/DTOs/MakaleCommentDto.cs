public class MakaleCommentDto
{
    public int Id { get; set; }
    public int MakaleId { get; set; }
    public string MakaleName { get; set; }  // Makale adını da göstermek için
    public int UserId { get; set; }
    public string UserName { get; set; }    // Kullanıcı adını göstermek için
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
} 