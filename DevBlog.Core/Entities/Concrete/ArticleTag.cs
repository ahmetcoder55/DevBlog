using DevBlog.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Core.Entities.Concrete
{
    public class ArticleTag:BaseEntity,IEntity
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
