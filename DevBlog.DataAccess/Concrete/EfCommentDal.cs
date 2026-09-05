using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.DataAccess.Concrete;
using DevBlog.Core.Entities.Concrete;
using DevBlog.DataAccess.Concrete.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.DataAccess.Concrete
{
    public class EfCommentDal : GenericRepository<Comment, AppDbContext>, ICommentDal
    {
        public EfCommentDal(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetApprovedCommentsByArticleIdAsync(int articleId)
        {
            return await _context.Comments
                .Where(c => c.ArticleId == articleId && c.IsApproved)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetPendingCommentsAsync()
        {
            return await _context.Comments
                .Where(c => !c.IsApproved)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }
    }
}
