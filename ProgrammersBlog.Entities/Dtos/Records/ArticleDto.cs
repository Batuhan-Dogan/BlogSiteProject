using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record ArticleDto
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden fazla olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden az olamaz.")]
        public string Title { get; init; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        [MinLength(20, ErrorMessage = "{0} alanı {1} karakterden az olamaz.")]
        public string Content { get; init; }
        [DisplayName("Thumbnail")]
        public string? Thumbnail { get; set; } = "articleImages/default.png";
        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden fazla olamaz.")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden az olamaz.")]
        public string SeoAuthor { get; init; }
        [DisplayName("Seo Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden fazla olamaz.")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden az olamaz.")]
        public string SeoDescription { get; init; }
        [DisplayName("Seo Etiketler")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden fazla olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden az olamaz.")]
        public string SeoTags { get; init; }
        [DisplayName("Aktif mi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public bool IsActive { get; init; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public int CategoryId { get; init; }
        public Category? Category { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
