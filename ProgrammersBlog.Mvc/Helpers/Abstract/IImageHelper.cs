using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;

namespace ProgrammersBlog.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picturFile"></param>
        /// <param name="pictureTypes"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        Task<IDataResult<ImageUploadedDto>> UploadImage(string name, IFormFile picturFile, PictureTypes pictureTypes, string folderName= null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureName"></param>
        /// <returns></returns>
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
