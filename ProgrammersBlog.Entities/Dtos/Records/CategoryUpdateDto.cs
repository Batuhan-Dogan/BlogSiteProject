using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record CategoryUpdateDto: CategoryDto
    {
        [DisplayName("Kategori Id")]
        [Required(ErrorMessage = "{0} boş olamaz.")]
        public int Id { get; init; }
        [DisplayName("Silindi mi ?")]
        public bool IsDeleted { get; init; }
    }
}
