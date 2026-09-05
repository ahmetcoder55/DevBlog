namespace DevBlog.Business.DTOs
{
    public class ArticleCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
