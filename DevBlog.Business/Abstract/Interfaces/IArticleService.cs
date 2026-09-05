using DevBlog.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Business.Abstract.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllArticlesAsync();
        Task<ArticleDto?> GetArticleByIdAsync(int id);
        Task<ArticleDto?> GetArticleBySlugAsync(string slug);
        Task CreateArticleAsync(ArticleCreateDto articleCreateDto);
        Task DeleteArticleAsync(int id);

        Task UpdateArticleAsync(ArticleUpdateDto dto);
    }
}
