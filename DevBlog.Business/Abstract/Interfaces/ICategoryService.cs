using DevBlog.Business.DTOs;

namespace DevBlog.Business.Abstract.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CategoryCreateDto dto);
        Task UpdateCategoryAsync(CategoryUpdateDto dto);
        Task DeleteCategoryAsync(int id);
    }
}
