using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.DataAccess.Concrete;
using DevBlog.Core.Entities.Concrete;
using DevBlog.DataAccess.Concrete.Contexts;

namespace DevBlog.DataAccess.Concrete
{
    public class EfTagDal : GenericRepository<Tag,AppDbContext>, ITagDal
    {
        public EfTagDal(AppDbContext context) : base(context)
        {
        }
    }
}
