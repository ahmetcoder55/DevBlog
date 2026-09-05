using DevBlog.Business.Abstract.Interfaces;
using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using DevBlog.Business.Concrete.Services;
using DevBlog.Core.DataAccess.Abstract;

namespace DevBlog.Business.Concrete;

public class ServiceManager : IServiceManager
{
   
    private readonly Lazy<IArticleService> _articleService;
    private readonly Lazy<ICategoryService> _categoryService;

    private readonly Lazy<ICommentService> _commentService;

    public ServiceManager(IUnitOfWork unitOfWork)
    {
        _articleService = new Lazy<IArticleService>(() => new ArticleManager(unitOfWork));
        _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(unitOfWork));
        _commentService = new Lazy<ICommentService>(() => new CommentManager(unitOfWork));
    }

    public IArticleService Articles => _articleService.Value;
    public ICategoryService Categories => _categoryService.Value;

    public ICommentService Comments => _commentService.Value;
}