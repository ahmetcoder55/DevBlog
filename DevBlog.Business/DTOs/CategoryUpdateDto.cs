using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Business.DTOs
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
