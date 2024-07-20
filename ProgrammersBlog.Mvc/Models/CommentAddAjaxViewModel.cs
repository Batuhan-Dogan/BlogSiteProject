using ProgrammersBlog.Entities.Dtos.Records;

namespace ProgrammersBlog.Mvc.Models
{
    public class CommentAddAjaxViewModel
    {
        public CommentAddDto CommentAddDto { get; set; }    
        public string CommentAddPartial { get; set; }
    }
}
