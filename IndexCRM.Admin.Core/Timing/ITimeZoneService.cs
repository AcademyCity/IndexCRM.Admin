using System.Threading.Tasks;
using Abp.Configuration;

namespace IndexCRM.Admin.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
