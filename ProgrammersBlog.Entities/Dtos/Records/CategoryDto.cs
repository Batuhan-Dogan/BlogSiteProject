using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record CategoryDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden büyük olamaz .")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olamaz.")]
        public string Name { get; init; }
        [DisplayName("Kategori açıklaması")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(500, ErrorMessage = "{0} {1} karaketerden fazla olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olamaz.")]
        public string Description { get; init; }
        [DisplayName("Kategori özel not alanı")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(500, ErrorMessage = "{0} {1} karaketerden fazla olamaz.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olamaz.")]
        public string Note { get; init; }
        [DisplayName("Aktif mi ?")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        public bool IsActive { get; set; }
    }
}
