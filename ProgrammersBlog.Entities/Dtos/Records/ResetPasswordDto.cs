using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record ResetPasswordDto
    {
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} bos geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }
        [DataType(DataType.Password)]
        [DisplayName("Eski şifre")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [Required(ErrorMessage = "{0} zorunlu alandır.")]
        public string CurrentPassword { get; init; }
        [DataType(DataType.Password)]
        [DisplayName("Yeni şifre")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [Required(ErrorMessage = "{0} zorunlu alandır.")]
        public string NewPassword { get; init; }
        [DataType(DataType.Password)]
        [DisplayName("Şifre doğrulama")]
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmedi")]
        public string NewPasswordConfirm { get; init; }
    }
}
