using DevBlog.Core.Entities.Concrete;

namespace DevBlog.Core.DataAccess.Abstract
{
    public interface ICategoryDal : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithArticlesAsync(int categoryId);
    }
}
