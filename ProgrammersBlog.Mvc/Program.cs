using ProgrammersBlog.Mvc.Infrastructure.Extensions;
using ProgrammersBlog.Mvc.AutoMapper.Profiles;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProgrammersBlog.Mvc.Filters;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(opt => opt.Filters.Add<MvcExceptionFilter>()).AddJsonOptions(
    opt => {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

    }
    );
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile), typeof(CommentProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.LoadMyServices(builder.Configuration.GetConnectionString("LocalDb"));
Log.Logger = new LoggerConfiguration()
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("LocalDb"),
        tableName: "Logs",
        columnOptions:ColumnOptions.Default,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Account/LogIn");
    options.LogoutPath = new PathString("/Account/LogOut");
    options.Cookie = new CookieBuilder
    {
        Name = "ProgrammersBlog",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = System.TimeSpan.FromDays(15);
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
});
var app = builder.Build();
//app.UseStatusCodePages();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{Id?}"
        );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Article}/{action=Index}/{Id?}");
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.UseHttpsRedirection();
app.Run();
