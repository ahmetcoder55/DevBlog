using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ArticleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("Article/Detail/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var article = await _serviceManager.Articles.GetArticleBySlugAsync(slug);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
    }
}
