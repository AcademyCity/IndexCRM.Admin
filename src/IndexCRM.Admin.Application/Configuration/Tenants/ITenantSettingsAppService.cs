using System.Threading.Tasks;
using Abp.Application.Services;
using IndexCRM.Admin.Configuration.Tenants.Dto;

namespace IndexCRM.Admin.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
