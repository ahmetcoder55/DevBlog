using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevBlog.Business.DTOs
{
    public class CommentCreateDto
    {
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad Soyad en fazla 50 karakter olabilir.")]
        public string AuthorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string AuthorEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yorum içeriği boş bırakılamaz.")]
        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        public string Content { get; set; } = string.Empty;
    }
}
