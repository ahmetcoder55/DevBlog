using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.DataAccess.Concrete;
using DevBlog.Core.Entities.Concrete;
using DevBlog.DataAccess.Concrete.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.DataAccess.Concrete
{
    public class EfArticleDal:GenericRepository<Article,AppDbContext>,IArticleDal
    {
        public EfArticleDal(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Article>> GetArticlesWithCategoryAsync()
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<Article?> GetBySlugWithDetailsAsync(string slug)
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .Include(a => a.Comments.Where(c => c.IsApproved))
                .FirstOrDefaultAsync(a => a.Slug == slug);
        }
    }
}
