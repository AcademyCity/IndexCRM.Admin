using Abp.AutoMapper;
using IndexCRM.Admin.Authorization.Users;
using IndexCRM.Admin.Authorization.Users.Dto;
using IndexCRM.Admin.Web.Areas.Mpa.Models.Common;

namespace IndexCRM.Admin.Web.Areas.Mpa.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; private set; }

        public UserPermissionsEditViewModel(GetUserPermissionsForEditOutput output, User user)
        {
            User = user;
            output.MapTo(this);
        }
    }
}