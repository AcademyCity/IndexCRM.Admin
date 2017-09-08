using Abp.Application.Services;
using IndexCRM.Admin.Tenants.Dashboard.Dto;

namespace IndexCRM.Admin.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();
    }
}
