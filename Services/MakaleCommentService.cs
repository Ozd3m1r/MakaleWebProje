using Entities.Models;
using Repositories;
using Repositories.InterfaceClass;
using Services.Contracts;
using System.Collections.Generic;

namespace Services
{
    public class MakaleCommentService : IMakaleCommentService
    {
        private readonly IRepositoryManager _repository;

        public MakaleCommentService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public IEnumerable<MakaleComment> GetCommentsByMakaleId(int makaleId, bool trackChanges)
        {
            return _repository.MakaleComment.GetCommentsByMakaleId(makaleId, trackChanges);
        }

        public IEnumerable<MakaleComment> GetCommentsByUserId(int userId, bool trackChanges)
        {
            return _repository.MakaleComment.GetCommentsByUserId(userId, trackChanges);
        }

        public void AddComment(MakaleComment comment)
        {
            _repository.MakaleComment.AddComment(comment);
        }

        public void UpdateComment(MakaleComment comment)
        {
            _repository.MakaleComment.UpdateComment(comment);
        }

        public void DeleteComment(int commentId, bool trackChanges)
        {
            _repository.MakaleComment.DeleteComment(commentId, trackChanges);
        }

        public MakaleComment GetCommentById(int commentId, bool trackChanges)
        {
            return _repository.MakaleComment.GetCommentById(commentId, trackChanges);
        }

        public int GetCommentCountByMakaleId(int makaleId, bool trackChanges)
        {
            return _repository.MakaleComment.GetCommentCountByMakaleId(makaleId, trackChanges);
        }

        public IEnumerable<MakaleComment> GetRecentComments(int count, bool trackChanges)
        {
            return _repository.MakaleComment.GetRecentComments(count, trackChanges);
        }

        public IEnumerable<MakaleComment> GetActiveCommentsByMakaleId(int makaleId, bool trackChanges)
        {
            return _repository.MakaleComment.GetActiveCommentsByMakaleId(makaleId, trackChanges);
        }
    }
} 