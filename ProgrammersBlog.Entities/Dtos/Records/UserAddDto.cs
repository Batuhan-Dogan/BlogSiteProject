using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record UserAddDto : UserDto
    {
        [DisplayName("Sifre")]
        [Required(ErrorMessage = "{0} bos geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; init; }
        [DisplayName("Resim")]
        [DataType(DataType.Upload)]
        public override IFormFile? PictureFile { get; init; }
        public override string Picture { get; set; } = "userImages/default.png";
    }
}
