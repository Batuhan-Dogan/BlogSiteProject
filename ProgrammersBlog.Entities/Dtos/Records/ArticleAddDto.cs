using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record ArticleAddDto: ArticleDto
    {
        [DisplayName("Thumbnail Foto")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public  IFormFile ThumbnailFile { get; set; }
    }
}
