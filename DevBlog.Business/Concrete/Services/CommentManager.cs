using DevBlog.Business.Abstract.Interfaces;
using DevBlog.Business.DTOs;
using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Business.Concrete.Services
{
    public class CommentManager:ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCommentAsync(CommentCreateDto dto)
        {
            var comment = new Comment
            {
                ArticleId = dto.ArticleId,
                AuthorName = dto.AuthorName,
                AuthorEmail = dto.AuthorEmail,
                Content = dto.Content,
                IsApproved = false, 
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentDto>> GetApprovedCommentsByArticleIdAsync(int articleId)
        {
            var comments = await _unitOfWork.Comments.GetApprovedCommentsByArticleIdAsync(articleId);

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                AuthorName = c.AuthorName,
                Content = c.Content,
                CreatedDate = c.CreatedDate
            });
        }

        public async Task ApproveCommentAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            if (comment != null)
            {
                comment.IsApproved = true;
                _unitOfWork.Comments.Update(comment);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteCommentAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            if (comment != null)
            {
                comment.IsDeleted = true;
                _unitOfWork.Comments.Update(comment);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    
    }
}
