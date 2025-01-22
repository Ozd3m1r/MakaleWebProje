using Entities.Models;
using Repositories.InterfaceClass;

public interface IMakaleCommentRepository : IRepositoryBase<MakaleComment>
{
    // Makaleye ait tüm yorumları getir
    IEnumerable<MakaleComment> GetCommentsByMakaleId(int makaleId, bool trackChanges);

    // Kullanıcının yaptığı tüm yorumları getir
    IEnumerable<MakaleComment> GetCommentsByUserId(int userId, bool trackChanges);

    // Yorum ekle
    void AddComment(MakaleComment comment);

    // Yorum güncelle
    void UpdateComment(MakaleComment comment);

    // Yorum sil (soft delete)
    void DeleteComment(int commentId, bool trackChanges);

    // Yorumu getir
    MakaleComment GetCommentById(int commentId, bool trackChanges);

    // Makaleye ait yorum sayısını getir
    int GetCommentCountByMakaleId(int makaleId, bool trackChanges);

    // Son yorumları getir
    IEnumerable<MakaleComment> GetRecentComments(int count, bool trackChanges);

    // Aktif yorumları getir
    IEnumerable<MakaleComment> GetActiveCommentsByMakaleId(int makaleId, bool trackChanges);

    //Kullanıcı Id göre silme işlemi 

    public void DeleteCommentsByUserId(int userId);

    IEnumerable<MakaleComment> GetAllMakaleComment(bool trackChanges);
}