namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int ActiveCategoryCount { get; set; }
        public int DeletedCategoryCount { get; set; }
        public int CategoryCount { get; set; }

        public int ActiveArticleCount { get; set; }
        public int DeletedArticleCount { get; set; }
        public int ArticleCount { get; set; }

        public int ActiveCommentCount { get; set; }
        public int DeletedCommentCount { get; set; }
        public int CommentCount { get; set; }

    }
}
