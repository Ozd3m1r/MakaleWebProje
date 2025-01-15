using Entities.Models;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IMakaleCommentService
    {
        IEnumerable<MakaleComment> GetCommentsByMakaleId(int makaleId, bool trackChanges);
        IEnumerable<MakaleComment> GetCommentsByUserId(int userId, bool trackChanges);
        void AddComment(MakaleComment comment);
        void UpdateComment(MakaleComment comment);
        void DeleteComment(int commentId, bool trackChanges);
        MakaleComment GetCommentById(int commentId, bool trackChanges);
        int GetCommentCountByMakaleId(int makaleId, bool trackChanges);
        IEnumerable<MakaleComment> GetRecentComments(int count, bool trackChanges);
        IEnumerable<MakaleComment> GetActiveCommentsByMakaleId(int makaleId, bool trackChanges);
    }
} 