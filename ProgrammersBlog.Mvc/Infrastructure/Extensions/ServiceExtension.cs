using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Mvc.Helpers.Concrete;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ProgrammersBlog.Mvc.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<ProgrammersBlogContext>(options=>options.UseNpgsql(connectionString));
            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 0;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase= false;
                opt.Password.RequireUppercase= false;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyzqABCDEFGHIJKLMNOPQRSTVUVYZ0123456789-$+@_";
            }).AddEntityFrameworkStores<ProgrammersBlogContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IArticleService, ArticleManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IServiceManager, ServiceManager> ();
            services.AddScoped<IImageHelper, ImageHelper>();
            return services;
        }
    }
}
