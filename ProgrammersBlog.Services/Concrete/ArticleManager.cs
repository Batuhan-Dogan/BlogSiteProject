using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
    public class ArticleManager : ManagerBase, IArticleService
    {
        #region Constractors
        public ArticleManager(IUnitOfWork unitOfWork,IMapper mapper):base(mapper,unitOfWork)
        {

        }
        #endregion

        #region Methods
        public async Task<IDataResult<ArticleDtoForGetOne>> GetAsync(int articleId)
        {
            var article = await UnitOfWork.ArticleRepository.GetAsync(a => a.Id.Equals(articleId));
            var b = article.Comments;
            if (article is not null)
            {
                return new DataResult<ArticleDtoForGetOne>(ResultStatus.Success, new ArticleDtoForGetOne()
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<ArticleDtoForGetOne>(ResultStatus.Error, Message.ArticleMessages.NotFound(false), new ArticleDtoForGetOne()
            {
                Article = null,
                ResultStatus = ResultStatus.Error,
                Message = Message.ArticleMessages.NotFound(false)
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllAsync()
        {
            var articles = await UnitOfWork.ArticleRepository.GetAllAsync(null);
            if (articles.Count > 0)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Info, new ArticleListDto() { Articles = articles, ResultStatus = ResultStatus.Success });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Message.ArticleMessages.NotFound(true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var category = await UnitOfWork.CategoryRepository.GetAsync(c => c.Id.Equals(categoryId));
            if (category is not null)
            {
                var articles = await UnitOfWork.ArticleRepository.GetAllAsync(a => a.CategoryId.Equals(categoryId));
                if (articles.Count > 0)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto()
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, Message.ArticleMessages.NotFoundByCategory(), null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Message.CategoryMessages.NotFound(false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByIsDeletedAsync(bool isDeleted)
        {
            var articles = await UnitOfWork.ArticleRepository.GetAllAsync(a => a.IsDeleted.Equals(isDeleted));
            if (articles.Count > 0)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Message.ArticleMessages.NotFound(isPlural: true),
                new ArticleListDto()
                {
                    Articles = null,
                    Message = Message.ArticleMessages.NotFound(isPlural: true),
                    ResultStatus = ResultStatus.Error
                }
                );
        }

        public async Task<IDataResult<ArticleDtoForGetOne>> GetArticleWithUserAndCategoryAsync(int articleId, bool isActive)
        {
            var article = await UnitOfWork.ArticleRepository.GetAsync(a => a.Id.Equals(articleId) && a.IsActive == true, a => a.User, a => a.Category);
            article.Comments = await UnitOfWork.CommentRepository.GetAllByRequestParametersAsync(new CommentRequestParameters()
            {
                ArticleId = article.Id,
                PageNumber = 1,
                PageSize = 12

            }, c => c.IsActive.Equals(isActive));
            if (article is not null)
            {
                await IncreaseViewCountAsync(articleId);
                return new DataResult<ArticleDtoForGetOne>(ResultStatus.Success, new ArticleDtoForGetOne()
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<ArticleDtoForGetOne>(ResultStatus.Error, Message.ArticleMessages.NotFound(false), new ArticleDtoForGetOne()
            {
                Article = null,
                ResultStatus = ResultStatus.Error,
                Message = Message.ArticleMessages.NotFound(false)
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetArticleSummariesByRequestParameters(ArticleRequestParameters requestParameters, bool isActive)
        {
            var articles = await UnitOfWork.ArticleRepository.GetAllSelectedMembersByRequestParameters(requestParameters, a => new Article
            {
                Id = a.Id,
                Thumbnail = a.Thumbnail,
                Title = a.Title,
                CreatedByName = a.CreatedByName,
                CreatedTime = a.CreatedTime,
                ModifiedByName = a.ModifiedByName,
                ModifiedTime = a.ModifiedTime,
                SeoDescription = a.SeoDescription,
            }
            , isActive);
            if (articles.Count > 0)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Info, new ArticleListDto() { Articles = articles, ResultStatus = ResultStatus.Success });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Message.ArticleMessages.NotFound(true), new ArticleListDto()
            {
                Articles = null,
                ResultStatus = ResultStatus.Error,
                Message = Message.ArticleMessages.NotFound(true)
            });

        }

        public async Task<IDataResult<ArticleListDto>> GetArticlesListPropertiesByRequestParameters(ArticleRequestParameters requestParameters, bool IsActive)
        {
            var articles = await UnitOfWork.ArticleRepository.GetAllSelectedMembersByRequestParameters(requestParameters, a => new Article
            {
                Id = a.Id,
                Title = a.Title,
            }
            , IsActive);
            if (articles.Count > 0)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Info, new ArticleListDto() { Articles = articles, ResultStatus = ResultStatus.Success });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Message.ArticleMessages.NotFound(true), new ArticleListDto()
            {
                Articles = null,
                ResultStatus = ResultStatus.Error,
                Message = Message.ArticleMessages.NotFound(true)
            });

        }

        public async Task<IResult> AddAsync(ArticleAddDto articleDto, string createdByName)
        {
            Article article = Mapper.Map<Article>(articleDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.Date = article.Date.ToUniversalTime();
            article.CreatedTime = article.CreatedTime.ToUniversalTime();
            article.ModifiedTime = article.ModifiedTime.ToUniversalTime();
            await UnitOfWork.ArticleRepository.AddAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success,Message.ArticleMessages.Added(article.Title));
        }

        public async Task<IResult> DeleteAsync(int articleId, string modifiedByName)
        {
            var result = await UnitOfWork.ArticleRepository.AnyAsync(a => a.Id.Equals(articleId));
            if (result)
            {
                Article article=await UnitOfWork.ArticleRepository.GetAsync(a=>a.Id.Equals(articleId));
                article.IsDeleted = true;
                article.ModifiedByName=modifiedByName;
                article.ModifiedTime=DateTime.Now;
                await UnitOfWork.ArticleRepository.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Message.ArticleMessages.Deleted(article.Title));
            }
            return new Result(ResultStatus.Error,Message.ArticleMessages.NotFound(false));
        }

        public async Task<IResult> HardDeleteAsync(int articleId)
        {
            var result =await UnitOfWork.ArticleRepository.AnyAsync(a => a.Id.Equals(articleId));
            if (result)
            {
                var article=await UnitOfWork.ArticleRepository.GetAsync(a=>a.Id.Equals(articleId));
                await UnitOfWork.ArticleRepository.DeleteAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,Message.ArticleMessages.HardDeleted(article.Title));
            }
            return new Result(ResultStatus.Error,"Böyle bir makale bulunamadı");
        }

        public async Task<IResult> UpdateAsync(ArticleUpdateDto articleDto, string modifiedByName)
        {
            Article article = Mapper.Map<Article>(articleDto);
            article.ModifiedByName= modifiedByName;
            article.Date = article.Date.ToUniversalTime();
            article.CreatedTime=article.CreatedTime.ToUniversalTime();
            article.ModifiedTime=article.ModifiedTime.ToUniversalTime();
            await UnitOfWork.ArticleRepository.UpdateAsync(article);

            await UnitOfWork.SaveAsync(); 
            return new Result(ResultStatus.Success,Message.ArticleMessages.Updated(article.Title));
        }

        public async Task<IResult> IncreaseViewCountAsync(int articleId)
        {
            var article= await UnitOfWork.ArticleRepository.GetAsync(ar => ar.Id.Equals(articleId));
            if(article is not null)
            {
                article.ViewsCount += 1;
                var updatedArticle=await UnitOfWork.ArticleRepository.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,Message.ArticleMessages.IncreasedViewCount());
            }
            return new Result(ResultStatus.Error,Message.ArticleMessages.NotFound(false));

        }

        public async Task<int> GetAllSelectedMembersCountsync(ArticleRequestParameters requestParameters)
        {
            return await UnitOfWork.ArticleRepository.GetAllSelectedMembersCount(requestParameters);
        }

        public async Task<int> GetActiveDataCountAsync()
        {
            return await UnitOfWork.ArticleRepository.CountAsync(a => a.IsActive.Equals(true));
        }

        public async Task<int> GetDeletedDataCountAsync()
        {
            return await UnitOfWork.ArticleRepository.CountAsync(a => a.IsDeleted.Equals(true));
        }

        public async Task<int> CountAsync()
        {
            return await UnitOfWork.ArticleRepository.CountAsync();
        }

        #endregion
    }
}
 