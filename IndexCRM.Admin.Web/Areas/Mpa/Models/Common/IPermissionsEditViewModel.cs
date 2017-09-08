using System.Collections.Generic;
using IndexCRM.Admin.Authorization.Permissions.Dto;

namespace IndexCRM.Admin.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}