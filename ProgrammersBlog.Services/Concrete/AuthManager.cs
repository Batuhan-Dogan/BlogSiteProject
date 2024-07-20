using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Classes;
using ProgrammersBlog.Entities.Dtos.Records;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities;
using ProgrammersBlog.Shared.Entities.Concrete;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes.Concrete;
using System.Security.Claims;
using static ProgrammersBlog.Services.Utilities.Message;

namespace ProgrammersBlog.Services.Concrete
{
    public class AuthManager : IAuthService
    {
        #region Fields
        private readonly IMapper _mapper;
        public  UserManager<User> UserManager { get; }
        public  RoleManager<Role> RoleManager { get; }
        #endregion
        #region Constractors
        public  AuthManager(IMapper mapper,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            UserManager = userManager;
            RoleManager = roleManager;
        }
        #endregion
        #region Methods

        public async Task<IResult> AddUserAsync(UserAddDto userAddDto)
        {
            Entities.Concrete.User user = _mapper.Map<User>(userAddDto);
            var result= await UserManager.CreateAsync(user,userAddDto.Password);
            if (result.Succeeded  )
            {
                if (userAddDto.Roles is not null)
                {
                   await UserManager.AddToRolesAsync(user, userAddDto.Roles);
                }
                else
                {
                    await UserManager.AddToRoleAsync(user, "user");
                }
                return new Result(ResultStatus.Success, Message.UserMessages.Added(user.UserName, true));
            }
            else
            {
                return new Result(ResultStatus.Error, Message.UserMessages.Added(user.UserName, false));
            }

        }
        public async Task<IDataResult<UsersListDto>> GetAllUsersInDataResultObjectAsync()
        {
            var users = await UserManager.Users.ToListAsync();
            if(users.Count>-1)
            {
                return new DataResult<UsersListDto>(ResultStatus.Success, new UsersListDto() {ResultStatus=ResultStatus.Success, Message = "", Users = users }) ;
            }
            else
            {
                return new DataResult<UsersListDto>(ResultStatus.Error,Message.UserMessages.NotFound(isPlural:true) ,new UsersListDto() { ResultStatus=ResultStatus.Error,Users=null,Message= Message.UserMessages.NotFound(isPlural: true) });

            }

        }

        public async Task<IDataResult<string>>HardDeleteUserAsync(int userId, int logInUserId)
        {
            User user = await UserManager.FindByIdAsync(userId.ToString());
            if (user.Id.Equals(logInUserId))
            {
                return new DataResult<string>(ResultStatus.Error, Message.UserMessages.CanNotBeDeletedLogInUser());
            }
            var isDeleted= await UserManager.DeleteAsync(user);
            if (isDeleted.Succeeded)
            {
                return new DataResult<string>(ResultStatus.Success, Message.UserMessages.Deleted(user.UserName, true), user.Picture);
            }
            return new DataResult<string>(ResultStatus.Error, Message.UserMessages.Deleted(user.UserName, false),user.Picture);
        }
        public async Task<IDataResult<UserDtoForGetOne>> GetUserInDataResultObjectAsync(int userId)
        {
            User user =  await UserManager.FindByIdAsync(userId.ToString());
            if(user is not null)
            {
                return new DataResult<UserDtoForGetOne>(ResultStatus.Success, new UserDtoForGetOne()
                {
                    ResultStatus = ResultStatus.Success,
                    User = user,
                    UserRoles= new HashSet<string>(await UserManager.GetRolesAsync(user)),
                    Roles= new HashSet<string>(await RoleManager.Roles.Select(r => r.Name).ToListAsync())
                });    
            }
            else
            {
                return new DataResult<UserDtoForGetOne>(ResultStatus.Error, Message.UserMessages.NotFound(isPlural: false), new UserDtoForGetOne() { ResultStatus = ResultStatus.Error, Message = Message.UserMessages.NotFound(isPlural:false), User = null });
            }
        }

        public async Task<IResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
             User oldUser = await UserManager.FindByIdAsync(userUpdateDto.Id.ToString());
            if (oldUser == null)
            {
                return new Result (ResultStatus.Error,Message.UserMessages.NotFound(false));
            }

            User updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
            var result = await UserManager.UpdateAsync(updatedUser);
            if (result.Succeeded )
            {
                if(userUpdateDto.Roles.Count > 0)
                {
                    IEnumerable<string> oldRoles = await UserManager.GetRolesAsync(updatedUser);
                    var oldUsersRolesDeleted = await UserManager.RemoveFromRolesAsync(updatedUser, oldRoles);
                    if (oldUsersRolesDeleted.Succeeded)
                    {
                        var rolesAdded = await UserManager.AddToRolesAsync(updatedUser, userUpdateDto.Roles);

                        if (rolesAdded.Succeeded)
                        {
                            return new Result(ResultStatus.Success, Message.UserMessages.Updated(updatedUser.UserName,true));
                        }
                    }
                    else
                    {
                        return new Result(ResultStatus.Error, Message.RoleMessages.UpdateFailed());
                    }
                }
                return new Result(ResultStatus.Success, Message.UserMessages.Updated(updatedUser.UserName,true));
            }

            return new Result(ResultStatus.Error, Message.UserMessages.Updated(updatedUser.UserName,false));

        }

        public async Task<IDataResult<User>> ChangePasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var roles = await RoleManager.Roles.ToListAsync();
            User user = await UserManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user == null)
            {
                return new DataResult<User>(ResultStatus.Error, Message.InCorrectDataEntry(), null);
            }

            if (!await UserManager.CheckPasswordAsync(user, resetPasswordDto.CurrentPassword))
            {
                return new DataResult<User>(ResultStatus.Error, Message.InCorrectDataEntry(), null);
            }

            var isPasswordRemoved = await UserManager.RemovePasswordAsync(user);
            if (!isPasswordRemoved.Succeeded)
            {
                return new DataResult<User>(ResultStatus.Error, Message.UserMessages.PasswordChanged(false), null);
            }

            var result = await UserManager.AddPasswordAsync(user, resetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                await UserManager.UpdateSecurityStampAsync(user);
                return new DataResult<User>(ResultStatus.Success,Message.UserMessages.PasswordChanged(true), user);
            }
            else
            {
                return new DataResult<User>(ResultStatus.Error, Message.UserMessages.PasswordChanged(false), null);
            }
        }

        public async Task<string> GetUserNameAsync(ClaimsPrincipal claims)
        {
            string userName = await Task.Run(() => UserManager.GetUserName(claims));
            return userName;
        }
        public async Task<IResult> ChangeSocialBladesAsync(ClaimsPrincipal claims, SocialBlades socialBlades)
        {
            var user =await UserManager.GetUserAsync(claims);
            user.LinkedInProfile = socialBlades.LinkedInProfileUrl;
            user.GitHubProfile = socialBlades.GitHubProfileUrl;
            var result =await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new Result(ResultStatus.Success, Message.UserMessages.Updated(user.UserName, true));
            }
            else
            {
                return new Result(ResultStatus.Error, Message.UserMessages.Updated(user.UserName, false));
            }
        }
        #endregion
    }
}
