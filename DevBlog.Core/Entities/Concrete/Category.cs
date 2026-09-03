using DevBlog.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Core.Entities.Concrete
{
    public class Category:BaseEntity,IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
