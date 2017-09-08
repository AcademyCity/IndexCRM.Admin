using System.Collections.Generic;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Authorization.Permissions.Dto;

namespace IndexCRM.Admin.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}