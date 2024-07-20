using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Services.Abstract;

namespace ProgrammersBlog.Mvc.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div" ,Attributes = "MostRead")]
    public class MostReadTagHelper:TagHelper
    {
        private readonly IServiceManager _serviceManager;
        public MostReadTagHelper(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        private readonly ArticleRequestParameters requestParameters = new ArticleRequestParameters() { PageNumber = 1, PageSize = 5, OrderBy = OrderOptions.MostRead };
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder div = new TagBuilder("div");

            TagBuilder h6 = new TagBuilder("h6");
            h6.Attributes.Add("class", "text-uppercase mb-4 font-weight-bold");

            TagBuilder icon = new TagBuilder("i");
            icon.Attributes.Add("class", "fa-solid fa-book");
            h6.InnerHtml.AppendHtml(icon);

            h6.InnerHtml.AppendHtml(" Çok Okunanlar ");
            div.InnerHtml.AppendHtml(h6);
            var articles = await _serviceManager.ArticleService.GetArticlesListPropertiesByRequestParameters(requestParameters,true);
            foreach (var article in articles.Data.Articles)
            {
                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href", $"/Article/Get/{article.Id}");
                a.Attributes.Add("class", "text-white link-offset-2 link-underline link-underline-opacity-0");
                a.InnerHtml.AppendHtml(article.Title);
                TagBuilder p = new TagBuilder("p");
                p.InnerHtml.AppendHtml(a);
                div.InnerHtml.AppendHtml(p);
            }
            output.Content.AppendHtml(div);
        }
    }
}
