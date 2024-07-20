using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record ArticleUpdateDto: ArticleDto
    {
        [Required]
        public int Id { get; init; }
        [DisplayName("Silindi mi?")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public bool IsDeleted { get; init; }
        [Required]
        public string CreatedByName { get; init; }
        [DisplayName("Thumbnail Foto")]
        public virtual IFormFile? ThumbnailFile { get; set; }
        [Required]
        public DateTime CreatedTime { get; init; }


    }
}
