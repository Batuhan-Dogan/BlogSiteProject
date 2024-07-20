using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;
using System.IO;

namespace ProgrammersBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper:IImageHelper
    {
        #region Fields
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string _imgFolder = "img";
        private const string _articleFolderName = "postImages";
        private const string _userFolderName = "userImages";
        #endregion
        #region Constractors
        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }
        #endregion
        #region Methods
        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {
            string oldPictureName = pictureName.Split("/")[1];
            if (oldPictureName.Equals("default.png"))
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, null);
            }
            var fileToDelete = Path.Combine($"{_wwwroot}/{_imgFolder}/", pictureName);
            if(System.IO.File.Exists(fileToDelete))
            {
                var fileInfo=new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto()
                {
                    Exstension = fileInfo.Extension,
                    Size = fileInfo.Length,
                    FullName = pictureName,
                    Path = fileInfo.FullName
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {

                return new DataResult<ImageDeletedDto>(ResultStatus.Success,"Böyle bir resim bulnamadı.",null);
            }
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadImage(string name, IFormFile pictureFile, PictureTypes pictureTypes, string folderName = null)
        {
            if(folderName == null)
            {
                folderName=pictureTypes==PictureTypes.User?_userFolderName:_articleFolderName;
            }
            if (!Directory.Exists($"{_wwwroot}/{_imgFolder}/{folderName}")) 
            {
                Directory.CreateDirectory($"{_wwwroot}/{_imgFolder}/{folderName}");
            }
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{_wwwroot}/{_imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            ImageUploadedDto uploadedImageDto=new ImageUploadedDto() 
            {
                Exstension = fileExtension,
                Path=path,
                Size=pictureFile.Length,
                FullName = $"{folderName}/{newFileName}"
            };
            string message = pictureTypes == PictureTypes.User ? $"{name} adlı kullanıcının resmi başarıyla yüklenmiştir." : $"{name} adlı makalenin resmi başarıyla yüklenmiştir.";
            return new DataResult<ImageUploadedDto>( ResultStatus.Success, message, uploadedImageDto );
        }
    #endregion
    }
}
