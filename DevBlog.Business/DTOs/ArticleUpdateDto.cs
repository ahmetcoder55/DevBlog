using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Business.DTOs
{
    public class ArticleUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
