using DevBlog.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DevBlog.Core.Entities.Concrete
{
    public class Article:BaseEntity,IEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string ContentMarkdown { get; set; } = string.Empty;
        public string? ThumbnailUrl { get; set; }
        public int ViewCount { get; set; } = 0;
        public int EstimatedReadTimeMinutes { get; set; } = 1;
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
