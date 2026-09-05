using DevBlog.Core.Entities.Concrete;

namespace DevBlog.Core.DataAccess.Abstract
{
    public interface ICommentDal : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetApprovedCommentsByArticleIdAsync(int articleId);
        Task<IEnumerable<Comment>> GetPendingCommentsAsync();
    }
}
