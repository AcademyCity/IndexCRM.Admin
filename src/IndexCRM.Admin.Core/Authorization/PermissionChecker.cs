using Abp.Authorization;
using IndexCRM.Admin.Authorization.Roles;
using IndexCRM.Admin.Authorization.Users;
using IndexCRM.Admin.MultiTenancy;

namespace IndexCRM.Admin.Authorization
{
    /// <summary>
    /// Implements <see cref="PermissionChecker"/>.
    /// </summary>
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
