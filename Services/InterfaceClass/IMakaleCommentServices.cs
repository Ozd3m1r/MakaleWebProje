using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceClass
{
    public interface IMakaleCommentServices
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
        IEnumerable<MakaleComment> GetAllMakaleComment(bool trackChanges);
       
    }
}
