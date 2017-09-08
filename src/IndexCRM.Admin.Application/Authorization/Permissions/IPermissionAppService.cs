using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Authorization.Permissions.Dto;

namespace IndexCRM.Admin.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
