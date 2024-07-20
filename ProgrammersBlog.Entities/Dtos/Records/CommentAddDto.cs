using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record CommentAddDto:CommentDto
    {
        [DisplayName("Adınız")]
        public string CreatedByName { get; set; } = string.Empty;
    }
}
