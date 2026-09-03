using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.DataAccess.Concrete;
using DevBlog.Core.Entities.Concrete;
using DevBlog.DataAccess.Concrete.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.DataAccess.Concrete
{
    public class EfCategoryDal : GenericRepository<Category,AppDbContext>, ICategoryDal
    {
        public EfCategoryDal(AppDbContext context) : base(context)
        {
        }

        public async Task<Category?> GetCategoryWithArticlesAsync(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.Articles)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
