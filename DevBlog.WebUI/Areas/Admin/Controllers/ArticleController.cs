using DevBlog.Business.Abstract;
using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using DevBlog.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevBlog.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ArticleController : Controller
{
    private readonly IServiceManager _serviceManager;

    public ArticleController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var articles = await _serviceManager.Articles.GetAllArticlesAsync();
        return View(articles);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesAsync();
        return View(new ArticleCreateDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArticleCreateDto dto)
    {
        if (dto.CategoryId <= 0)
        {
            ModelState.AddModelError("CategoryId", "Lütfen geçerli bir kategori seçiniz.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(dto);
        }

        try
        {
            await _serviceManager.Articles.CreateArticleAsync(dto);
            TempData["SuccessMessage"] = "Makale başarıyla yayınlandı.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu: " + innerMsg);

            await PopulateCategoriesAsync();
            return View(dto);
        }
    }

    // GÜNCELLEME SAYFASI (GET)
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var articleDto = await _serviceManager.Articles.GetArticleByIdAsync(id);
        if (articleDto == null)
        {
            return NotFound();
        }

        var updateDto = new ArticleUpdateDto
        {
            Id = articleDto.Id,
            Title = articleDto.Title,
            Content = articleDto.Content,
            CategoryId = articleDto.CategoryId,
            ThumbnailUrl = articleDto.ThumbnailUrl
        };

        await PopulateCategoriesAsync();
        return View(updateDto);
    }

    // GÜNCELLEME İŞLEMİ (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ArticleUpdateDto dto)
    {
        if (dto.CategoryId <= 0)
        {
            ModelState.AddModelError("CategoryId", "Lütfen geçerli bir kategori seçiniz.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(dto);
        }

        try
        {
            await _serviceManager.Articles.UpdateArticleAsync(dto);
            TempData["SuccessMessage"] = "Makale güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            ModelState.AddModelError("", "Güncelleme Hatası: " + innerMsg);

            await PopulateCategoriesAsync();
            return View(dto);
        }
    }

    // SİLME İŞLEMİ (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _serviceManager.Articles.DeleteArticleAsync(id);
            TempData["SuccessMessage"] = "Makale silindi.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Silme Hatası: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCategoriesAsync(int? selectedId = null)
    {
        var categories = await _serviceManager.Categories.GetAllCategoriesAsync();
        ViewBag.Categories = categories ?? new List<CategoryDto>();
    }
}