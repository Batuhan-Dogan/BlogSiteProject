using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Shared.Entities.Concrete;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System.Security.Claims;


namespace ProgrammersBlog.Services.Abstract
{
    public interface IAuthService
    {
        UserManager<User> UserManager { get; }
        RoleManager<Role> RoleManager { get; }


        /// <summary>
        /// Varitabanına yeni kullanıcı ekler
        /// </summary>
        /// <param name="userAddDto">Eklenecek kullanıcının bilgilerini taşıyan record type</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> AddUserAsync(UserAddDto userAddDto);

        /// <summary>
        /// Veritabanından kullanıcı kaldırır
        /// </summary>
        /// <param name="userId">Veritabanından kaldırılacak kullanıcı ID değeri</param>
        /// <param name="logInUserId">Mevcut kullanıcı ID'si </param>
        /// <returns>İşlem sonucunu taşıyan IDataResult sonucu</returns>
        Task<IDataResult<string>> HardDeleteUserAsync(int userId,int logInUserId);

        /// <summary>
        /// Veritabanında bir kullanıcıyı günceller
        /// </summary>
        /// <param name="userUpdateDto">Gücel Kullanıcı bilgilerini taşıyan Record Type</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> UpdateUserAsync(UserUpdateDto userUpdateDto);

        /// <summary>
        /// Tüm kullanıcıları getirir
        /// </summary>
        /// <returns>Tüm kullanıcıları kapsayan IDataResult<UserListDto>sonucu</returns>
        Task<IDataResult<UsersListDto>> GetAllUsersInDataResultObjectAsync();

        /// <summary>
        /// Veritabanından ID değerine göre kullanıcı getirir
        /// </summary>
        /// <param name="userId">Getirilecek kullanıcının ID değeri</param>
        /// <returns>Kullanıcı bilgilerini taşıyan IDataResul sonucu</returns>
        Task<IDataResult<UserDtoForGetOne>> GetUserInDataResultObjectAsync(int userId);

        /// <summary>
        /// Kullanıcı parolasını günceller
        /// </summary>
        /// <param name="resetPasswordDto">Yeni parola bilgilerinin bulunduran record type</param>
        /// <returns>İşlem sonucunu taşıyan IDataResult</returns>
        Task<IDataResult<User>> ChangePasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<string>GetUserNameAsync(ClaimsPrincipal claims);

        /// <summary>
        /// Kullanıcın sosyal medya bilgilerini değişirir
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="socialBlades">Sosyal Medya bilgilerini bulunduran class</param>
        /// <returns>İşlem sonucunu taşıyan IResult sonucu</returns>
        Task<IResult> ChangeSocialBladesAsync(ClaimsPrincipal claims, SocialBlades socialBlades);
    }
}
