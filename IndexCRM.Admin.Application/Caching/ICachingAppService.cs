using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Caching.Dto;

namespace IndexCRM.Admin.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultDto<CacheDto> GetAllCaches();

        Task ClearCache(EntityDto<string> input);

        Task ClearAllCaches();
    }
}
