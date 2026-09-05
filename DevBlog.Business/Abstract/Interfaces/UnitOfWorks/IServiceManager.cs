using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Business.Abstract.Interfaces.UnitOfWorks
{
    public interface IServiceManager
    {
        IArticleService Articles { get; }
        ICategoryService Categories { get; }

        ICommentService Comments { get; }
    }
}
