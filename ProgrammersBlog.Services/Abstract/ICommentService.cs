using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<CommentDtoForGetOne>> GetAsync(int articleId);
        Task<IDataResult<CommentListDto>> GetAllAsync();
        Task<IResult> AddAsync(CommentAddDto commentAddDto);
        Task<IResult> DeleteAsync(int id, string createdByName);
        Task<IResult> HardDeleteAsync(int id);
        Task<IResult> UpdateAsync(CommentUpdateDto commentUpdateDto,string modifiedByName);
        Task<IDataResult<CommentListDto>> GetCommentByRequestParametersAsync(CommentRequestParameters requestParameters,bool isActive);
        Task<IDataResult<CommentListDto>> GetCommentByUserNameAsync( string userName,CommentRequestParameters requestParameters, bool isActive);
        Task<int> GetUserCommentCountAsync(string userName);
    }
}
