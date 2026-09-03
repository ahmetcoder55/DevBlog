using DevBlog.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Core.DataAccess.Abstract
{
    public interface IArticleDal:IGenericRepository<Article>
    {
        Task<IEnumerable<Article>> GetArticlesWithCategoryAsync();
        Task<Article?> GetBySlugWithDetailsAsync(string slug);
    }
}
