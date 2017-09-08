using System.Threading.Tasks;
using Abp.Application.Services;
using IndexCRM.Admin.Configuration.Host.Dto;

namespace IndexCRM.Admin.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
