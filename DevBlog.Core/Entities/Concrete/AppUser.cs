using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Core.Entities.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
