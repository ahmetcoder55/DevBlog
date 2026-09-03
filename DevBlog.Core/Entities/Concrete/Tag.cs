using DevBlog.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Core.Entities.Concrete
{
    public class Tag:BaseEntity,IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    }
}
