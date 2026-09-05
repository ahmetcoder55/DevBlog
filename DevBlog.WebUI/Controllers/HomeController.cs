using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using DevBlog.Business.Concrete;
using DevBlog.Core.Entities.Concrete;
using DevBlog.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevBlog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _manager;

        public HomeController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var articles = await _manager.Articles.GetAllArticlesAsync();

            if (categoryId.HasValue)
            {
                articles = articles.Where(a => a.CategoryId == categoryId.Value);
            }

            return View(articles);
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
