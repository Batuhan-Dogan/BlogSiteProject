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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProgrammersBlog.Services.Utilities.Message;

namespace ProgrammersBlog.Services.Concrete
{
    public class CommentManager: ManagerBase, ICommentService,IDataCountService
    {

        #region Constractors
        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper):base(mapper,unitOfWork)
        {

        }
        #endregion

        #region Methods
        public async Task<IResult> AddAsync(CommentAddDto commentAddDto)
        {
            var article=await UnitOfWork.ArticleRepository.GetAsync(ar=>ar.Id.Equals(commentAddDto.ArticleId));
            if(article is null)
            {
                return new Result(ResultStatus.Error, Message.CommentMessages.CouldNotAdd());
            }
            article.CommentCount =await UnitOfWork.CommentRepository.CountAsync(c => c.Id.Equals(article.Id)&&c.IsDeleted!=false);
            Entities.Concrete.Comment comment =Mapper.Map<Entities.Concrete.Comment>(commentAddDto);
            await UnitOfWork.CommentRepository.AddAsync(comment);
            await UnitOfWork.ArticleRepository.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Message.CommentMessages.Added());
        }


        public async Task<IResult> DeleteAsync(int id,string createdByName)
        {
            Entities.Concrete.Comment comment = await UnitOfWork.CommentRepository.GetAsync(c => c.Id.Equals(id),c=>c.Article);
            var article = comment.Article;

            if (comment == null)
            {
                return new Result(ResultStatus.Error, Message.CategoryMessages.NotFound(isPlural: false));
            }
            if (comment.IsDeleted.Equals(true))
            {
                return new Result(ResultStatus.Warning, Message.CommentMessages.AlreadyDeleted());
            }
            comment.IsDeleted = true;
            comment.IsActive = false;
            comment.ModifiedByName = createdByName;
            await UnitOfWork.CommentRepository.UpdateAsync(comment);
            article.CommentCount = await UnitOfWork.CommentRepository.CountAsync(c => c.Id.Equals(article.Id) && c.IsDeleted != false);
            await UnitOfWork.ArticleRepository.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result ( ResultStatus.Success, Message.CommentMessages.Deleted());
        }
        public async Task<IResult> HardDeleteAsync(int id)
        {
            var comment = await UnitOfWork.CommentRepository.GetAsync(c => c.Id.Equals(id), c => c.Article);
            var article = comment.Article;
            if (comment == null)
            {
                return new Result(ResultStatus.Error, Message.CategoryMessages.NotFound(isPlural: false));
            }
            await UnitOfWork.CommentRepository.DeleteAsync(comment);
            article.CommentCount = await UnitOfWork.CommentRepository.CountAsync(c => c.Id.Equals(article.Id) && c.IsDeleted != false);
            await UnitOfWork.ArticleRepository.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Message.CommentMessages.Deleted());
        }

        public async Task<IDataResult<CommentListDto>> GetAllAsync()
        {
            var comments=await UnitOfWork.CommentRepository.GetAllAsync(null,x=>x.Article);
            if (comments.Count() > 0)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Success,
                    
                }) ;                
            }
            return new DataResult<CommentListDto>(ResultStatus.Error, Message.CommentMessages.NotFound(true), new CommentListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = Message.CommentMessages.NotFound(true),
                Comments=null
            }) ;
        }

        public async Task<IDataResult<CommentDtoForGetOne>> GetAsync(int commentId)
        {
            var comment = await UnitOfWork.CommentRepository.GetAsync(x=>x.Id.Equals(commentId));
            if(comment is not null)
            {
                return new DataResult<CommentDtoForGetOne>(ResultStatus.Success, Message.CommentMessages.AlreadyDeleted(), new CommentDtoForGetOne
                {
                    Comment = comment,
                    ResultStatus = ResultStatus.Success,
                    Message = Message.CommentMessages.AlreadyDeleted()
                }) ;
            }
            return new DataResult<CommentDtoForGetOne>(ResultStatus.Success, Message.CommentMessages.NotFound(false), new CommentDtoForGetOne
            {
                Comment = null,
                ResultStatus = ResultStatus.Error,
                Message = Message.CommentMessages.NotFound(false)
            });
        }

        public async Task<IResult> UpdateAsync(CommentUpdateDto commentUpdateDto, string modifiedByName)
        {
            var comment = await UnitOfWork.CommentRepository.GetAsync(c => c.Id.Equals(commentUpdateDto.Id));
            if (comment == null)
            {
                return new Result(ResultStatus.Error, Message.CategoryMessages.NotFound(isPlural: false));
            }
            else
            {
                var updatedComment=Mapper.Map<CommentUpdateDto,Entities.Concrete.Comment>(commentUpdateDto,comment);
                updatedComment.ModifiedByName = modifiedByName;

                if (updatedComment.IsActive.Equals(true))
                {
                    updatedComment.IsDeleted = false;
                }

                await UnitOfWork.CommentRepository.UpdateAsync(updatedComment);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Message.CommentMessages.Updated());
            }
           
        }

        public async Task<int> CountAsync()
        {
            return await UnitOfWork.CommentRepository.CountAsync();
        }

        public async Task<int> GetActiveDataCountAsync()
        {
            return await UnitOfWork.CommentRepository.CountAsync(c => c.IsActive.Equals(true));
        }
        public async Task<int> GetDeletedDataCountAsync()
        {
            return await UnitOfWork.CommentRepository.CountAsync(c => c.IsDeleted.Equals(true));
        }

        public async Task<IDataResult<CommentListDto>> GetCommentByRequestParametersAsync(CommentRequestParameters requestParameters,bool isActive)
        {
            var comments =await UnitOfWork.CommentRepository.GetAllByRequestParametersAsync(requestParameters,c=>c.IsActive.Equals(isActive));
            if(comments.Count() > 0)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto()
                {
                    ResultStatus=ResultStatus.Success,
                    Comments=comments
                    
                });
            }
            else
            {
                return new DataResult<CommentListDto>(ResultStatus.Error, new CommentListDto()
                {
                    ResultStatus = ResultStatus.Error,
                    Comments = null,
                    Message = Message.CommentMessages.NotFound(true)
                });
            }
        }

        public async Task<int> GetUserCommentCountAsync(string userName)
        {
            return await UnitOfWork.CommentRepository.CountAsync(c => c.CreatedByName.Equals(userName));
        }

        public async Task<IDataResult<CommentListDto>> GetCommentByUserNameAsync(string userName, CommentRequestParameters requestParameters,bool isActive)
        {
            var comments =await UnitOfWork.CommentRepository.GetAllByRequestParametersAsync(requestParameters,c => c.CreatedByName.Equals(userName)&&c.IsActive.Equals(isActive));
            if (comments.Count > 0)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto() { ResultStatus = ResultStatus.Success, Comments = comments });
            }
            else
            {
                return new DataResult<CommentListDto>(ResultStatus.Error, Message.CommentMessages.NotFound(true), new CommentListDto() { ResultStatus = ResultStatus.Error, Comments = null,Message=Message.CommentMessages.NotFound(true) });
            }
        }
        #endregion
    }
}
