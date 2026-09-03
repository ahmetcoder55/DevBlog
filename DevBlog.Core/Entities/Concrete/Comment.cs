using DevBlog.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Core.Entities.Concrete
{
    public class Comment:BaseEntity,IEntity
    {
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false; 
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
    }
}
