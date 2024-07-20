using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record UserDto
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} bos geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kücük olmamalıdır.")]
        public string UserName { get; init; }
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} bos geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} bos gecilmemelidir.")]
        [MaxLength(13, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(13, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; init; }
        [DataType(DataType.Upload)]
        public virtual IFormFile? PictureFile { get; init; }
        public virtual string? Picture { get; set; } = "userImages/default.png";
        public HashSet<string>? Roles { get; init; }
    }
}
