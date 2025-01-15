using Entities.Models;
using Repositories;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System.Collections.Generic;

namespace Services
{
    public class MakaleCommentManager : IMakaleCommentServices
    {
        private readonly IRepositoryManager _manager;

        public MakaleCommentManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<MakaleComment> GetCommentsByMakaleId(int makaleId, bool trackChanges)
        {
            return _manager.MakaleComment.GetCommentsByMakaleId(makaleId, trackChanges);
        }

        public IEnumerable<MakaleComment> GetCommentsByUserId(int userId, bool trackChanges)
        {
            return _manager.MakaleComment.GetCommentsByUserId(userId, trackChanges);
        }

        public void AddComment(MakaleComment comment)
        {
            _manager.MakaleComment.AddComment(comment);
        }

        public void UpdateComment(MakaleComment comment)
        {
            _manager.MakaleComment.UpdateComment(comment);
        }

        public void DeleteComment(int commentId, bool trackChanges)
        {
            _manager.MakaleComment.DeleteComment(commentId, trackChanges);
        }

        public MakaleComment GetCommentById(int commentId, bool trackChanges)
        {
            return _manager.MakaleComment.GetCommentById(commentId, trackChanges);
        }

        public int GetCommentCountByMakaleId(int makaleId, bool trackChanges)
        {
            return  _manager.MakaleComment.GetCommentCountByMakaleId(makaleId, trackChanges);
        }

        public IEnumerable<MakaleComment> GetRecentComments(int count, bool trackChanges)
        {
            return _manager.MakaleComment.GetRecentComments(count, trackChanges);
        }

        public IEnumerable<MakaleComment> GetActiveCommentsByMakaleId(int makaleId, bool trackChanges)
        {
            return  _manager.MakaleComment.GetActiveCommentsByMakaleId(makaleId, trackChanges);
        }
    }
}