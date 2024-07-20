using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Exstensions
{
    public static class ArticleRepositoryExstension
    {
        public static IQueryable<Article> FilteredByCategoryId(this IQueryable<Article> articles, int? categoryId=0)
        {
            if (categoryId is null)
            {
                return articles;
            }

            return articles.Where(ar => ar.CategoryId.Equals( categoryId));
        }
        public static IQueryable<Article> FilteredBySearchTerm(this IQueryable<Article> articles, String? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return (articles);
            }
            else
            {
                return articles.Where(ar => ar.Title.ToLower().Contains(searchTerm.ToLower()));
            }
        }
        public static IQueryable<Article> FilteredByIsActive(this IQueryable<Article> articles, bool? check)
        {
            if (check.Equals(true))
            {
                return articles.Where(ar => ar.IsActive.Equals(true));

            }
            else
            {
                return articles;
            }
        }
        public static IQueryable<Article> ToPaginate(this IQueryable<Article> articles, int pageNumber, int pageSize)
        {
            return articles.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public static IQueryable<Article> Order(this IQueryable<Article> articles,string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return (articles);
            }
            if (orderBy.Equals(OrderOptions.HotlyDebated))
            {
                return articles.OrderByDescending(ar=>ar.CommentCount);
            }
            else if (orderBy.Equals(OrderOptions.MostRead))
            {
                return articles.OrderByDescending(ar => ar.ViewsCount);
            }
            else if (orderBy.Equals(OrderOptions.FirstAdded))
            {
                return articles.OrderBy(ar => ar.CreatedTime);
            }
            else if (orderBy.Equals(OrderOptions.LastAdded))
            {
                return articles.OrderByDescending(ar => ar.CreatedTime);
            }
            else 
            {
                return articles;
            }
        }
    }
}
