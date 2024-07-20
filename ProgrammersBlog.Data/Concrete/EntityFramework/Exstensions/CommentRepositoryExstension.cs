using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Exstensions
{
    public static class CommentRepositoryExstension
    {
        public static IQueryable<Comment> FilteredByArticleId(this IQueryable<Comment> comments, int? articleId)
        {
            if (articleId is null)
            {
                return comments;
            }

            return comments.Where(c => c.ArticleId.Equals(articleId));
        }
        public static IQueryable<Comment> FilteredByUserName(this IQueryable<Comment> comments, string? userName)
        {
            if (userName is null )
            {
                return comments;
            }

            return comments.Where(c => c.CreatedByName.Equals(userName));
        }
        public static IQueryable<Comment> FilteredBySearchTerm(this IQueryable<Comment> comments, String? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return (comments);
            }
            else
            {
                return comments.Where(c => c.Text.ToLower().Contains(searchTerm.ToLower()));
            }
        }
        public static IQueryable<Comment> FilteredByIsActive(this IQueryable<Comment> comments, bool? check)
        {
            if (check.Equals(true))
            {
                return comments.Where(c => c.IsActive.Equals(true));

            }
            else
            {
                return comments;
            }
        }
        public static IQueryable<Comment> ToPaginate(this IQueryable<Comment> comments, int pageNumber, int pageSize)
        {
            return comments.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public static IQueryable<Comment> Order(this IQueryable<Comment> comments, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return (comments);
            }
            if (orderBy.Equals(OrderOptions.FirstAdded))
            {
                return comments.OrderBy(c => c.CreatedTime);
            }
            else if (orderBy.Equals(OrderOptions.LastAdded))
            {
                return comments.OrderByDescending(c => c.CreatedTime);
            }
            else
            {
                return comments;
            }
        }
    }
}
