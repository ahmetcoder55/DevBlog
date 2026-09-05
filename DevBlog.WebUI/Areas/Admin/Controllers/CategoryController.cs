using DevBlog.Business.Abstract;
using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using DevBlog.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly IServiceManager _serviceManager;

    public CategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _serviceManager.Categories.GetAllCategoriesAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CategoryCreateDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            ModelState.AddModelError("Name", "Kategori adı boş bırakılamaz.");
            return View(dto);
        }

        try
        {
            await _serviceManager.Categories.CreateCategoryAsync(dto);
            TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var inner = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            ModelState.AddModelError("", "Veritabanı Kayıt Hatası: " + inner);
            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _serviceManager.Categories.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound();

        var dto = new CategoryUpdateDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _serviceManager.Categories.UpdateCategoryAsync(dto);
        TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _serviceManager.Categories.DeleteCategoryAsync(id);
        TempData["SuccessMessage"] = "Kategori silindi.";
        return RedirectToAction(nameof(Index));
    }
}