using DevBlog.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Business.Abstract.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentCreateDto dto);
        Task<IEnumerable<CommentDto>> GetApprovedCommentsByArticleIdAsync(int articleId);
        Task ApproveCommentAsync(int id);
        Task SoftDeleteCommentAsync(int id);
    }
}
