using DevBlog.Business.Abstract.Interfaces;
using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using DevBlog.Business.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private readonly IServiceManager _commentService;

        public CommentController(IServiceManager commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentCreateDto dto)
        {
            var article = await _commentService.Articles.GetArticleByIdAsync(dto.ArticleId);

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen tüm alanları doğru doldurunuz.";

                if (article != null)
                    return RedirectToAction("Detail", "Article", new { slug = article.Slug });

                return RedirectToAction("Index", "Home");
            }

            TempData["SuccessMessage"] = "Yorumunuz başarıyla gönderildi. Onaylandıktan sonra görünür olacaktır.";

            if (article != null)
            {
                return RedirectToAction("Detail", "Article", new { slug = article.Slug });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}