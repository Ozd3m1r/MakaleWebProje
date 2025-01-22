using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repositories.InterfaceClass;
using Entities.Models;

namespace Repositories.RepositoryClass
{
    public class MakaleCommentRepository : RepositoryBase<MakaleComment>, IMakaleCommentRepository
    {
        private readonly RepositoryContext _context;

        public MakaleCommentRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<MakaleComment> GetCommentsByMakaleId(int makaleId, bool trackChanges)
        {
            var query = _context.MakaleComment.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            return query.Include(c => c.Users)
                .Where(c => c.MakaleId == makaleId)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();
        }

        public IEnumerable<MakaleComment> GetCommentsByUserId(int userId, bool trackChanges)
        {
            var query = _context.MakaleComment.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            return query.Include(c => c.Makale)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();
        }

        public void AddComment(MakaleComment comment)
        {
            _context.MakaleComment.Add(comment);
            _context.SaveChanges();
        }

        public void UpdateComment(MakaleComment comment)
        {
            _context.MakaleComment.Update(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(int commentId, bool trackChanges)
        {
            var comment = GetCommentById(commentId, trackChanges);
            if (comment != null)
            {
                comment.IsActive = false;
                Delete(comment);
                _context.SaveChanges();
            }
        }

        public MakaleComment GetCommentById(int commentId, bool trackChanges)
        {
            var query = _context.MakaleComment.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            return query.Include(c => c.Users)
                .FirstOrDefault(c => c.MakaleCommentId == commentId);
        }

        public int GetCommentCountByMakaleId(int makaleId, bool trackChanges)
        {
            var query = _context.MakaleComment.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            return query.Count(c => c.MakaleId == makaleId && c.IsActive);
        }

        public IEnumerable<MakaleComment> GetRecentComments(int count, bool trackChanges)
        {
            var query = _context.MakaleComment.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            return query.Where(c => c.IsActive)
                .Include(c => c.Users)
                .Include(c => c.Makale)
                .OrderByDescending(c => c.CreatedDate)
                .Take(count)
                .ToList();
        }

        public IEnumerable<MakaleComment> GetActiveCommentsByMakaleId(int makaleId, bool trackChanges)
        {
            var query = _context.MakaleComment.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            return query.Where(c => c.MakaleId == makaleId && c.IsActive)
                .Include(c => c.Users)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();
        }
        public void DeleteCommentsByUserId(int userId)
        {
            var comments = _context.MakaleComment
                .Where(mc => mc.UserId == userId)
                .ToList();

            if (comments.Any())
            {
                _context.MakaleComment.RemoveRange(comments);
            }
        }
        public IEnumerable<MakaleComment> GetAllMakaleComment(bool trackChanges)
        {
            return FindAll(trackChanges).ToList();
        }
    }
}