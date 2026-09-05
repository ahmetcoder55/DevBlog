using DevBlog.Business.Abstract.Interfaces;
using DevBlog.Business.DTOs;
using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.Entities.Concrete;
using DevBlog.DataAccess.Concrete.UnitOfWorks;
using System.Text.RegularExpressions;

namespace DevBlog.Business.Concrete.Services;

public class CategoryManager : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        });
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }



    public async Task CreateCategoryAsync(CategoryCreateDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            Slug = GenerateSlug(dto.Name),
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        };

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    private string GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Guid.NewGuid().ToString();

        string str = title.ToLowerInvariant();

        str = str.Replace("ç", "c")
                 .Replace("ğ", "g")
                 .Replace("ı", "i")
                 .Replace("ö", "o")
                 .Replace("ş", "s")
                 .Replace("ü", "u");

        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = Regex.Replace(str, @"\s", "-");

        return string.IsNullOrEmpty(str) ? Guid.NewGuid().ToString() : str;
    }

    public async Task UpdateCategoryAsync(CategoryUpdateDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);
        if (category != null)
        {
            category.Name = dto.Name;
            category.Description = dto.Description;

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category != null)
        {
            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}