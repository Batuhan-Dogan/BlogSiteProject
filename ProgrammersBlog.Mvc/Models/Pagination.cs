namespace ProgrammersBlog.Mvc.Models
{
    public class Pagination
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 8;
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        public string OrderBy { get; set; }
    }
}
