using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Core.DataAccess.Abstract
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IArticleDal Articles { get; }
        ICategoryDal Categories { get; }
        ITagDal Tags { get; }
        ICommentDal Comments { get; }

        Task<int> SaveChangesAsync();
    }
}
