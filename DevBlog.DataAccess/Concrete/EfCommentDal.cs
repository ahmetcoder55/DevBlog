using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.DataAccess.Concrete;
using DevBlog.Core.Entities.Concrete;
using DevBlog.DataAccess.Concrete.Contexts;

namespace DevBlog.DataAccess.Concrete
{
    public class EfCommentDal : GenericRepository<Comment, AppDbContext>, ICommentDal
    {
        public EfCommentDal(AppDbContext context) : base(context)
        {
        }
    }
}
