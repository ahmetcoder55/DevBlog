using DevBlog.Business.Abstract;
using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly IServiceManager _serviceManager;

    public HomeController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _serviceManager.Articles.GetAllArticlesAsync();
        var categories = await _serviceManager.Categories.GetAllCategoriesAsync();

        ViewBag.TotalArticles = articles.Count();
        ViewBag.TotalCategories = categories.Count();
        ViewBag.RecentArticles = articles.OrderByDescending(a => a.CreatedDate).Take(5).ToList();

        return View();
    }
}