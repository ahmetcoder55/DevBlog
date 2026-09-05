using System.ComponentModel.DataAnnotations;

namespace DevBlog.Business.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
