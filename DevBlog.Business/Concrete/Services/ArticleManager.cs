using DevBlog.Business.Abstract.Interfaces;
using DevBlog.Business.DTOs;
using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.Entities.Concrete;
using System.Text.RegularExpressions;

namespace DevBlog.Business.Concrete.Services;

public class ArticleManager : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArticleManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ArticleDto>> GetAllArticlesAsync()
    {
        var articles = await _unitOfWork.Articles.GetArticlesWithCategoryAsync();

        return articles.Select(a => new ArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.ContentMarkdown,
            Slug = a.Slug,
            ThumbnailUrl = a.ThumbnailUrl,
            CategoryId = a.CategoryId,
            CategoryName = a.Category?.Name ?? "Kategorisiz",
            CreatedDate = a.CreatedDate
        });
    }

    public async Task<ArticleDto?> GetArticleByIdAsync(int id)
    {
        var article = await _unitOfWork.Articles.GetByIdAsync(id);
        if (article == null) return null;

        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.ContentMarkdown,
            Slug = article.Slug,
            ThumbnailUrl = article.ThumbnailUrl,
            CategoryId = article.CategoryId,
            CreatedDate = article.CreatedDate
        };
    }

    public async Task<ArticleDto?> GetArticleBySlugAsync(string slug)
    {
        var article = await _unitOfWork.Articles.GetBySlugWithDetailsAsync(slug);
        if (article == null) return null;

        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.ContentMarkdown,
            Slug = article.Slug,
            ThumbnailUrl = article.ThumbnailUrl,
            CategoryId = article.CategoryId,
            CategoryName = article.Category?.Name ?? "Kategorisiz",
            CreatedDate = article.CreatedDate
        };
    }

    public async Task CreateArticleAsync(ArticleCreateDto dto)
    {
        // Summary girilmediyse Content içinden ilk 150 karakteri otomatik al
        string generatedSummary = !string.IsNullOrWhiteSpace(dto.Summary)
            ? dto.Summary
            : (dto.Content.Length > 150 ? dto.Content.Substring(0, 150) + "..." : dto.Content);

        var article = new Article
        {
            Title = dto.Title ?? string.Empty,
            ContentMarkdown = dto.Content ?? string.Empty,
            Summary = generatedSummary,
            CategoryId = dto.CategoryId,
            ThumbnailUrl = dto.ThumbnailUrl,
            Slug = GenerateSlug(dto.Title ?? Guid.NewGuid().ToString()),
            ViewCount = 0,
            EstimatedReadTimeMinutes = Math.Max(1, (dto.Content?.Length ?? 0) / 500), // Tahmini okuma süresi
            IsPublished = true,
            PublishedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        };

        await _unitOfWork.Articles.AddAsync(article);
        await _unitOfWork.SaveChangesAsync();
    }

    private string GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return Guid.NewGuid().ToString();

        string str = title.ToLowerInvariant();
        str = str.Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i")
                 .Replace("ö", "o").Replace("ş", "s").Replace("ü", "u");
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = Regex.Replace(str, @"\s", "-");

        return string.IsNullOrEmpty(str) ? Guid.NewGuid().ToString() : str;
    }


    public async Task<ArticleDto?> GetByIdAsync(int id)
    {
        var article = await _unitOfWork.Articles.GetByIdAsync(id);
        if (article == null || article.IsDeleted) return null;

        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.ContentMarkdown,
            Slug = article.Slug,
            ThumbnailUrl = article.ThumbnailUrl,
            CategoryId = article.CategoryId,
            CategoryName = article.Category?.Name ?? string.Empty,
            CreatedDate = article.CreatedDate
        };
    }

    // GÜNCELLEME
    public async Task UpdateArticleAsync(ArticleUpdateDto dto)
    {
        var article = await _unitOfWork.Articles.GetByIdAsync(dto.Id);
        if (article == null || article.IsDeleted)
        {
            throw new Exception("Güncellenecek makale bulunamadı.");
        }

        article.Title = dto.Title ?? string.Empty;
        article.ContentMarkdown = dto.Content ?? string.Empty;
        article.Summary = !string.IsNullOrWhiteSpace(dto.Summary)
            ? dto.Summary
            : (dto.Content.Length > 150 ? dto.Content.Substring(0, 150) + "..." : dto.Content);
        article.CategoryId = dto.CategoryId;
        article.ThumbnailUrl = dto.ThumbnailUrl;
        article.Slug = GenerateSlug(dto.Title ?? article.Slug);
        article.UpdatedDate = DateTime.UtcNow;

        _unitOfWork.Articles.Update(article);
        await _unitOfWork.SaveChangesAsync();
    }

    // SİLME (Soft Delete)
    public async Task DeleteArticleAsync(int id)
    {
        var article = await _unitOfWork.Articles.GetByIdAsync(id);
        if (article == null)
        {
            throw new Exception("Silinecek makale bulunamadı.");
        }

        article.IsDeleted = true;
        article.UpdatedDate = DateTime.UtcNow;

        _unitOfWork.Articles.Update(article);
        await _unitOfWork.SaveChangesAsync();
    }
}
