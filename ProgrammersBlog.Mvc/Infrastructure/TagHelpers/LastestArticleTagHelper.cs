using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.Mvc.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "RecentlyAdded")]
    public class LastestArticleTagHelper : TagHelper
    {
        private readonly IServiceManager _serviceManager;
        public LastestArticleTagHelper(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        private readonly ArticleRequestParameters requestParameters = new ArticleRequestParameters
        {
            PageNumber = 1,
            PageSize = 5,
            OrderBy = OrderOptions.LastAdded
        };
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            var articles=await _serviceManager.ArticleService.GetArticlesListPropertiesByRequestParameters(requestParameters,true);
            TagBuilder div = new TagBuilder("div");
            TagBuilder listGroup = new TagBuilder("ul");
            listGroup.Attributes.Add("class", "list-group bg-dark");
            TagBuilder liTitle = new TagBuilder("li");
            liTitle.Attributes.Add("class", "list-group-item text-dark");
            liTitle.InnerHtml.AppendHtml(" Son Eklenenler ");
            div.InnerHtml.AppendHtml(liTitle);
            if (articles.Data.ResultStatus==0)
            {
                foreach(var article in articles.Data.Articles) 
                {
                    TagBuilder a = new TagBuilder("a");
                    a.Attributes.Add("class", "link-offset-2 link-underline link-underline-opacity-0");
                    a.Attributes.Add("href", $"/Article/Get/{article.Id}");
                    TagBuilder liItem = new TagBuilder("li");
                    liItem.Attributes.Add("class", "list-group-item text-primary");
                    liItem.InnerHtml.AppendHtml(article.Title);

                    a.InnerHtml.AppendHtml(liItem);
                    listGroup.InnerHtml.AppendHtml(a);
                }
                div.InnerHtml.AppendHtml(listGroup);
            }
            output.Content.AppendHtml(div);
        }
    }
}
