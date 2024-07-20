using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Records
{

    public record CommentDto
    {
        [DisplayName("Yorum")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(1000, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        public string Text { get; init; }
        [DisplayName("Makale Seçiniz")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public int ArticleId { get; init; }
    }
}