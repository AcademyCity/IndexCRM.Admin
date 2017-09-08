using System.Collections.Generic;
using IndexCRM.Admin.Authorization.Users.Dto;

namespace IndexCRM.Admin.Web.Areas.Mpa.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}