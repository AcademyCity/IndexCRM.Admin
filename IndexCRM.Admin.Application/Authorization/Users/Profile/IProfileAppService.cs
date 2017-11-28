using System.Threading.Tasks;
using Abp.Application.Services;
using IndexCRM.Admin.Authorization.Users.Profile.Dto;

namespace IndexCRM.Admin.Authorization.Users.Profile
{
    public interface IProfileAppService : IApplicationService
    {
        Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit();

        Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input);
        
        Task ChangePassword(ChangePasswordInput input);

        Task UpdateProfilePicture(UpdateProfilePictureInput input);

        string UpdateCouponPicture(UpdateProfilePictureInput input);

        Task<GetPasswordComplexitySettingOutput> GetPasswordComplexitySetting();
    }
}
